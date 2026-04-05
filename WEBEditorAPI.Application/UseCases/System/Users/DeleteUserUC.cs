using AutoMapper;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System.Users;

public class DeleteUserUC(IUserRepository userRepository, IMapper mapper) : IUseCase<DeleteRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> ExecuteAsync(DeleteRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (user == null)
            throw new ApiNotFoundException("Usuário não encontrado");
        if (user.Id == request.Context.UserId)
            throw new ApiBadRequestException("Não é permitido deletar a própria conta");
        user.Delete();
        await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserDto>(user);
    }
}