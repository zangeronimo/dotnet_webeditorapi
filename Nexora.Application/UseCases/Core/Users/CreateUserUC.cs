using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Users;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.ValueObjects;

namespace Nexora.Application.UseCases.Core.Users;

public class CreateUserUC(IUserRepository userRepository, IPasswordProvider passwordProvider, IMapper mapper) : IUseCase<CreateUserRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDto> ExecuteAsync(CreateUserRequest request)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user != null)
            throw new ApiBadRequestException(UserErrors.EmailAlreadyRegistered);
        User newUser = new User(request.Name, Email.Create(request.Email), Password.Create(request.Password, _passwordProvider), request.Status);
        await _userRepository.AddAsync(newUser);
        User? createdUser = await _userRepository.GetByIdAsync(newUser.Id);
        return _mapper.Map<UserDto>(createdUser);
    }
}
