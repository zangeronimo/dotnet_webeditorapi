using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Users;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Application.UseCases.Core.Users;

public class CreateUserUC(IUserRepository userRepository, IPasswordProvider passwordProvider, IMapper mapper) : IUseCase<CreateUserRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDto> ExecuteAsync(CreateUserRequest request)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user != null)
            throw new ApiBadRequestException(UserErrors.EmailAlreadyRegistered);
        User newUser = new User(request.Name, Email.Create(request.Email), Password.Create(request.Password, _passwordProvider), request.Status);
        await _userRepository.AddAsync(newUser);
        User? createdUser = await _userRepository.GetByIdAsync(newUser.Id);
        return _mapper.Map<UserDto>(createdUser);
    }
}
