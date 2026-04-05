using AutoMapper;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System.Users;

public class GetUserByIdUC(IUserRepository userRepository, IMapper mapper) : IUseCase<GetByIdRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> ExecuteAsync(GetByIdRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (user == null)
            throw new ApiNotFoundException("Usuário não encontrado");
        return _mapper.Map<UserDto>(user);
    }
}