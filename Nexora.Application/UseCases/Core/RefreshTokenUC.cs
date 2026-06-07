using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.Core;

public class RefreshTokenUC : IRefreshToken
{
    private readonly IUserRepository _userRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ITokenProvider _tokenProvider;

    public RefreshTokenUC(
        IUserRepository userRepository,
        IUserCompanyRepository userCompanyRepository,
        IPermissionRepository permissionRepository,
        ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _userCompanyRepository = userCompanyRepository;
        _permissionRepository = permissionRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthResponse> ExecuteAsync(string refresh)
    {
        var payload = _tokenProvider.ValidateRefreshToken(refresh);
        var user = await _userRepository.GetByIdAsync(payload.UserId)
            ?? throw new ApiInvalidCredentialsException();
        var userCompanies = await _userCompanyRepository.GetByUserIdAsync(user.Id);
        if (!userCompanies.Any())
            throw new ApiInvalidCredentialsException();
        var selectedCompany = userCompanies.FirstOrDefault(x => x.CompanyId == payload.CompanyId);
        if (selectedCompany == null)
            throw new ApiInvalidCredentialsException();
        var permissions = await _permissionRepository.GetByUserCompanyAsync(selectedCompany.Id);
        var token = _tokenProvider.GenerateToken(user.Id, user.Email.Value, permissions, selectedCompany.CompanyId, TokenType.Access)
            ?? throw new ApiBadRequestException(AuthErrors.CreateJwtError);
        var refreshToken = _tokenProvider.GenerateToken(user.Id, user.Email.Value, permissions, selectedCompany.CompanyId, TokenType.Refresh)
            ?? throw new ApiBadRequestException(AuthErrors.CreateJwtError);
        return new AuthResponse()
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }
}