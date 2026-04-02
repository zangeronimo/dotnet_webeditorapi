using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System;

public class RefreshTokenUC : IRefreshToken
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;

    public RefreshTokenUC(IUserRepository userRepository, ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthResponse> ExecuteAsync(string refresh)
    {
        var payload = _tokenProvider.ValidateToken(refresh);
        var User = await _userRepository.GetByIdAsync(payload.UserId, payload.CompanyId) ?? throw new ApiInvalidCredentialsException();
        var token = _tokenProvider.GenerateToken(User.Id, User.Email.Value, User.CompanyId, TokenType.Access);
        if (string.IsNullOrEmpty(token))
        {
            throw new ApiBadRequestException("Falha ao gerar JWT");
        }
        var refreshToken = _tokenProvider.GenerateToken(User.Id, User.Email.Value, User.CompanyId, TokenType.Refresh);
        if (string.IsNullOrEmpty(token))
        {
            throw new ApiBadRequestException("Falha ao gerar JWT");
        }
        return new AuthResponse()
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }
}
