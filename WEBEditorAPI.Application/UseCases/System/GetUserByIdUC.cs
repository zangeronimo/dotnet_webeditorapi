using AutoMapper;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Models.System;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System;

public class GetUserByIdUC(IUserRepository userRepository, IMapper mapper) : IUseCase<GetUserByIdRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> ExecuteAsync(GetUserByIdRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, request.CompanyId);
        if (user == null)
            throw new ApiNotFoundException("Usuário não encontrado");
        return _mapper.Map<UserDto>(user);
    }
}