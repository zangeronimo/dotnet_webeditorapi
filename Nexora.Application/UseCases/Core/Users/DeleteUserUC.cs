using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Users;

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
