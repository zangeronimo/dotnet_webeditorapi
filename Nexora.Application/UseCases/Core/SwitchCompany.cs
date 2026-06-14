using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.Core;

public class SwitchCompanyUC : ISwitchCompany
{
    private readonly IUserRepository _userRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ITokenProvider _tokenProvider;

    public SwitchCompanyUC(
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

    public async Task<AuthResponse> ExecuteAsync(RequestContext context)
    {
        var userCompanies = await _userCompanyRepository.GetByUserIdAsync(context.UserId);
        if (!userCompanies.Any())
            throw new ApiInvalidCredentialsException();
        var selectedCompany = userCompanies.FirstOrDefault(x => x.CompanyId == context.CompanyId);
        if (selectedCompany == null || selectedCompany.User == null)
            throw new ApiInvalidCredentialsException();
        var permissions = await _permissionRepository.GetByUserCompanyAsync(selectedCompany.Id);
        var token = _tokenProvider.GenerateToken(selectedCompany.User.Id, selectedCompany.User.Email.Value, permissions, selectedCompany.CompanyId, TokenType.Access)
            ?? throw new ApiBadRequestException(AuthErrors.CreateJwtError);
        var refreshToken = _tokenProvider.GenerateToken(selectedCompany.User.Id, selectedCompany.User.Email.Value, permissions, selectedCompany.CompanyId, TokenType.Refresh)
            ?? throw new ApiBadRequestException(AuthErrors.CreateJwtError);
        selectedCompany.MakeLogin();
        await _userCompanyRepository.UpdateAsync(selectedCompany);
        return new AuthResponse()
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }
}