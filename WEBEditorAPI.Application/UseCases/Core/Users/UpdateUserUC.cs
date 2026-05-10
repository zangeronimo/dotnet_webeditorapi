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

public class UpdateUserUC(IUserRepository userRepository, IPasswordProvider passwordProvider, IMapper mapper) : IUseCase<UpdateUserRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDto> ExecuteAsync(UpdateUserRequest request)
    {
        User? emailExists = await _userRepository.GetByEmailAsync(request.Email);
        if (emailExists != null && emailExists.Id != request.Id)
            throw new ApiBadRequestException(UserErrors.EmailAlreadyRegistered);
        User? user = await _userRepository.GetByIdAsync((Guid)request.Id);
        if (user == null || request.Id == Guid.Empty)
            throw new ApiNotFoundException(UserErrors.NotFound);
        user!.Update(request.Name, Email.Create(request.Email), request.Status);
        if (!string.IsNullOrEmpty(request.Password))
            user.UpdatePassword(Password.Create(request.Password, _passwordProvider));
        await _userRepository.UpdateAsync(user);
        User? updatedUser = await _userRepository.GetByIdAsync(user.Id);
        return _mapper.Map<UserDto>(updatedUser);
    }
}
