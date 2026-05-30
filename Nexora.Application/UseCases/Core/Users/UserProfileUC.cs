using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.DTOs.System;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Users;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Enums;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;
using Nexora.Domain.ValueObjects;

namespace Nexora.Application.UseCases.Core.Users;

public class UserProfileUC(IUserRepository userRepository, IUserCompanyRepository userCompanyRepository, IPasswordProvider passwordProvider, IMapper mapper) : IUseCase<UserProfileRequest, UserProfileDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<UserProfileDto> ExecuteAsync(UserProfileRequest request)
    {
        User? emailExists = await _userRepository.GetByEmailAsync(request.Email);
        if (emailExists != null && emailExists.Id != request.Context.UserId)
            throw new ApiBadRequestException(UserErrors.EmailAlreadyRegistered);
        User? user = await _userRepository.GetByIdAsync((Guid)request.Context.UserId);
        if (user == null || user.Status == Status.Inactive)
            throw new ApiNotFoundException(UserErrors.NotFound);

        var userCompany = await _userCompanyRepository.GetByUserIdAsync(request.Context.UserId);
        UserCompany? selectedUserCompany = userCompany.FirstOrDefault(uc => uc.CompanyId == request.Context.CompanyId);
        if (selectedUserCompany == null || selectedUserCompany.Status == Status.Inactive)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);

        user!.Update(request.Name, Email.Create(request.Email), Status.Active);
        if (!string.IsNullOrEmpty(request.Password))
            user.UpdatePassword(Password.Create(request.Password, _passwordProvider));
        await _userRepository.UpdateAsync(user);

        selectedUserCompany!.Update(request.NickName, selectedUserCompany.AvatarUrl, Status.Active);
        await _userCompanyRepository.UpdateAsync(selectedUserCompany);

        User? updatedUser = await _userRepository.GetByIdAsync(user.Id);
        UserDto userDto = _mapper.Map<UserDto>(updatedUser);

        UserCompany? updatedUserCompany = await _userCompanyRepository.GetByIdAsync(selectedUserCompany.Id, request.Context.CompanyId);
        UserCompanyDto? userCompanyDto = _mapper.Map<UserCompanyDto>(updatedUserCompany);

        return new UserProfileDto() { User = userDto, UserCompany = userCompanyDto };
    }
}
