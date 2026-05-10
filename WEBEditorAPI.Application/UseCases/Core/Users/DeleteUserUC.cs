using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Users;

public class DeleteUserUC(IUserRepository userRepository, IMapper mapper) : IUseCase<DeleteRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> ExecuteAsync(DeleteRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.ResourceId);
        if (user == null)
            throw new ApiNotFoundException(UserErrors.NotFound);
        if (user.Id == request.Context.UserId)
            throw new ApiBadRequestException(UserErrors.DeleteOwnAccountNotAllowed);
        user.Delete();
        await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserDto>(user);
    }
}
