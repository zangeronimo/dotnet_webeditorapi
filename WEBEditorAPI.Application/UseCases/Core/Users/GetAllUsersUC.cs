using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Users;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Users;

public class GetAllUsersUC(IUserRepository userRepository, IMapper mapper) : IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<UserDto>> ExecuteAsync(GetAllUsersFilterRequest request)
    {
        (IEnumerable<User> users, int total) = await _userRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Email, request.Status);

        return new PaginationResult<UserDto>
        {
            Items = _mapper.Map<IEnumerable<UserDto>>(users),
            Total = total
        };
    }
}
