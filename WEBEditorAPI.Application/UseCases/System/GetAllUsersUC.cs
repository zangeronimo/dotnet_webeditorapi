using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Models.System;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Application.UseCases.System;

public class GetAllUsersUC(IUserRepository userRepository, IMapper mapper) : IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<UserDto>> ExecuteAsync(GetAllUserFilterModel request)
    {
        (IEnumerable<User> users, int total) = await _userRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Email, request.CompanyId);

        return new PaginationResult<UserDto>
        {
            Items = _mapper.Map<IEnumerable<UserDto>>(users),
            Total = total
        };
    }
}
