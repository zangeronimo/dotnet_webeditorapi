using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Users;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Enums;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.Core.Users;

public class UserProfileAvatarUC(IUserCompanyRepository userCompanyRepository, IStorageProvider storageProvider) : IUseCase<UserProfileAvatarRequest, UserProfileAvatarDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IStorageProvider _storageProvider = storageProvider;
    public async Task<UserProfileAvatarDto> ExecuteAsync(UserProfileAvatarRequest request)
    {
        var userCompany = await _userCompanyRepository.GetByUserIdAsync(request.Context.UserId);
        UserCompany? selectedUserCompany = userCompany.FirstOrDefault(uc => uc.CompanyId == request.Context.CompanyId);
        if (selectedUserCompany == null || selectedUserCompany.Status == Status.Inactive)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);
        var avatarUrl = await _storageProvider.SaveStreamAsync(request.Avatar, request.Context.CompanyId.ToString(), "avatar");
        selectedUserCompany!.Update(selectedUserCompany.NickName, avatarUrl, Status.Active);
        await _userCompanyRepository.UpdateAsync(selectedUserCompany);
        UserCompany? updatedUserCompany = await _userCompanyRepository.GetByIdAsync(selectedUserCompany.Id, request.Context.CompanyId);
        return new UserProfileAvatarDto() { AvatarUrl = updatedUserCompany?.AvatarUrl };
    }
}
