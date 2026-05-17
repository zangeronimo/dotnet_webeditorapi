using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Users;

public class GetUserByIdUC(IUserRepository userRepository, IMapper mapper) : IUseCase<GetByIdRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> ExecuteAsync(GetByIdRequest request)
    {
        User? user = await _userRepository.GetByIdAsync(request.ResourceId);
        if (user == null)
            throw new ApiNotFoundException(UserErrors.NotFound);
        return _mapper.Map<UserDto>(user);
    }
}