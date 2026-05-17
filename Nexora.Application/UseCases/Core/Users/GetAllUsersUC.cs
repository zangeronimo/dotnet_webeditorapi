using AutoMapper;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Users;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Users;

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
