using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.UserCompanies;

public class CreateUserCompanyUC(IUserRepository userRepository, IUserCompanyRepository userCompanyRepository, IMapper mapper) : IUseCase<CreateUserCompanyRequest, UserCompanyDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<UserCompanyDto> ExecuteAsync(CreateUserCompanyRequest request)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new ApiBadRequestException(UserErrors.NotFound);
        var userCompanies = await _userCompanyRepository.GetByUserIdAsync(user.Id);
        var userExists = userCompanies.FirstOrDefault(uc => uc.UserId == user.Id && uc.CompanyId == request.Context.CompanyId);
        if (userExists != null)
            throw new ApiBadRequestException(UserCompanyErrors.AlreadyExists);
        UserCompany newUserCompany = new UserCompany(user.Id, request.Context.CompanyId, user.Name, null, Status.Inactive);
        newUserCompany.SetInvite();
        newUserCompany.SetJoined();
        await _userCompanyRepository.AddAsync(newUserCompany);
        UserCompany? createdUserCompany = await _userCompanyRepository.GetByIdAsync(newUserCompany.Id, request.Context.CompanyId);
        return _mapper.Map<UserCompanyDto>(createdUserCompany);
    }
}
