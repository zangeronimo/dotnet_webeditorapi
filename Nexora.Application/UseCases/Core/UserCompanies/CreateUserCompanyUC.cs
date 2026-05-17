using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.UserCompanies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.UserCompanies;

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
