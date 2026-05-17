using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core;

public class MakeLoginUC : IMakeLogin
{
    private readonly IUserRepository _userRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPasswordProvider _passwordProvider;
    private readonly ITokenProvider _tokenProvider;

    public MakeLoginUC(
        IUserRepository userRepository,
        IUserCompanyRepository userCompanyRepository,
        IPermissionRepository permissionRepository,
        IPasswordProvider passwordProvider,
        ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _userCompanyRepository = userCompanyRepository;
        _permissionRepository = permissionRepository;
        _passwordProvider = passwordProvider;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthResponse> ExecuteAsync(AuthRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email!);
        if (user == null || user.Status == Status.Inactive)
            throw new ApiInvalidCredentialsException();
        if (_passwordProvider.Validate(request.Password!, user.PasswordHash.Hash) == false)
        {
            throw new ApiInvalidCredentialsException();
        }
        var userCompanies = await _userCompanyRepository.GetByUserIdAsync(user.Id);
        var activeUserCompanies = userCompanies
            .Where(x => x.Status == Status.Active
                && x.Company!.Status == Status.Active
                && x.User!.Status == Status.Active)
            .ToList();
        if (!activeUserCompanies.Any())
            throw new ApiInvalidCredentialsException();
        var selectedCompany = activeUserCompanies.OrderByDescending(x => x.LastAccessedAt).First();
        var permissions = await _permissionRepository.GetByUserCompanyAsync(selectedCompany.Id);
        var token = _tokenProvider.GenerateToken(user.Id, user.Email.Value, permissions, selectedCompany.CompanyId, TokenType.Access);
        if (string.IsNullOrEmpty(token))
        {
            throw new ApiBadRequestException("Falha ao gerar JWT");
        }
        var refreshToken = _tokenProvider.GenerateToken(user.Id, user.Email.Value, permissions, selectedCompany.CompanyId, TokenType.Refresh);
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new ApiBadRequestException("Falha ao gerar JWT");
        }
        selectedCompany.MakeLogin();
        await _userCompanyRepository.UpdateAsync(selectedCompany);
        return new AuthResponse()
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }
}
