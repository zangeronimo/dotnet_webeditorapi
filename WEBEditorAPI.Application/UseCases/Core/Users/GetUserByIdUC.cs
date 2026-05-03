using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Users;

public class GetUserByIdUC(IUserRepository userRepository, IMapper mapper) : IUseCase<GetByIdRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> ExecuteAsync(GetByIdRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.ResourceId);
        if (user == null)
            throw new ApiNotFoundException("Usuário não encontrado");
        return _mapper.Map<UserDto>(user);
    }
}