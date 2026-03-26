using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System;

public class MakeLoginUC : IUseCase<AuthRequest, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordProvider _passwordProvider;
    private readonly ITokenProvider _tokenProvider;

    public MakeLoginUC(IUserRepository userRepository, IPasswordProvider passwordProvider, ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _passwordProvider = passwordProvider;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthResponse> ExecuteAsync(AuthRequest request)
    {
        var User = await _userRepository.GetByEmailAsync(request.Username) ?? throw new ApiInvalidCredentialsException();
        if (_passwordProvider.Validate(request.Password, User.Password.Hash, User.Password.Salt) == false)
        {
            throw new ApiInvalidCredentialsException();
        }
        var token = _tokenProvider.GenerateToken(User.Id, User.Email.Value, User.CompanyId, TokenType.Access);
        if (string.IsNullOrEmpty(token))
        {
            throw new ApiBadRequestException("Falha ao gerar JWT");
        }
        var refreshToken = _tokenProvider.GenerateToken(User.Id, User.Email.Value, User.CompanyId, TokenType.Refresh);
        if (string.IsNullOrEmpty(refreshToken))
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
