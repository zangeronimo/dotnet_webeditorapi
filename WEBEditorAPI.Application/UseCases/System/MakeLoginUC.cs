using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System;

public class MakeLoginUC : IUseCase<AuthRequest, AuthResponse>
{
    private readonly IUserRepository UserRepository;
    private readonly IPasswordProvider PasswordProvider;

    public MakeLoginUC(IUserRepository userRepository, IPasswordProvider passwordProvider)
    {
        UserRepository = userRepository;
        PasswordProvider = passwordProvider;
    }

    public async Task<AuthResponse> ExecuteAsync(AuthRequest request)
    {
        var User = await UserRepository.GetByEmailAsync(request.Username) ?? throw new ApiInvalidCredentialsException();
        if (PasswordProvider.Validate(request.Password, User.Password.Hash, User.Password.Salt) == false)
        {
            throw new ApiInvalidCredentialsException();
        }
        return new AuthResponse()
        {
            User = new UserResponse(User),
            Token = "Token 123",
            RefreshToken = "Refresh Token 456"
        };
    }
}
