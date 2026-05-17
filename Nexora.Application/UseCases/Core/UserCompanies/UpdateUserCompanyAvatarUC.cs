using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.UserCompanies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.UserCompanies;

public class UpdateUserCompanyAvatarUC(IUserCompanyRepository userCompanyRepository, IStorageProvider storageProvider, IMapper mapper) : IUseCase<UpdateUserCompanyAvatarRequest, UserCompanyDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IStorageProvider _storageProvider = storageProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<UserCompanyDto> ExecuteAsync(UpdateUserCompanyAvatarRequest request)
    {
        UserCompany? userCompany = await _userCompanyRepository.GetByIdAsync(request.UserCompanyId, request.Context.CompanyId);
        if (userCompany == null)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);
        var avatarUrl = await _storageProvider.SaveStreamAsync(request.Avatar, request.Context.CompanyId.ToString(), "avatar");
        userCompany!.Update(userCompany.NickName, avatarUrl, userCompany.Status);
        await _userCompanyRepository.UpdateAsync(userCompany);
        UserCompany? updatedUserCompany = await _userCompanyRepository.GetByIdAsync(userCompany.Id, request.Context.CompanyId);
        return _mapper.Map<UserCompanyDto>(updatedUserCompany);
    }
}
