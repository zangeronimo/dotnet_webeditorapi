using AutoMapper;
using Nexora.Application.DTOs.System;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.UserCompanies;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.UserCompanies;

public class UpdateUserCompanyUC(IUserCompanyRepository userCompanyRepository, IMapper mapper) : IUseCase<UpdateUserCompanyRequest, UserCompanyDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<UserCompanyDto> ExecuteAsync(UpdateUserCompanyRequest request)
    {
        UserCompany? userCompany = await _userCompanyRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (userCompany == null)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);
        userCompany!.Update(request.NickName, userCompany.AvatarUrl, request.Status);
        await _userCompanyRepository.UpdateAsync(userCompany);
        UserCompany? updatedUserCompany = await _userCompanyRepository.GetByIdAsync(userCompany.Id, request.Context.CompanyId);
        return _mapper.Map<UserCompanyDto>(updatedUserCompany);
    }
}
