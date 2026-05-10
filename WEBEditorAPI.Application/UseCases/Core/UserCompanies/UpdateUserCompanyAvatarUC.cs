using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.UserCompanies;

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
