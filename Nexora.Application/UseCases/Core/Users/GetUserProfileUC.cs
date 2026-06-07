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
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.Core.Users;

public class GetUserProfileUC(IUserRepository userRepository, IUserCompanyRepository userCompanyRepository, IMapper mapper) : IUseCase<GetUserProfileRequest, UserProfileDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<UserProfileDto> ExecuteAsync(GetUserProfileRequest request)
    {
        User? user = await _userRepository.GetByIdAsync((Guid)request.Context.UserId);
        if (user == null || user.Status == Status.Inactive)
            throw new ApiNotFoundException(UserErrors.NotFound);
        var userCompanies = await _userCompanyRepository.GetByUserIdAsync(request.Context.UserId);
        UserCompany? selectedUserCompany = userCompanies.FirstOrDefault(uc => uc.CompanyId == request.Context.CompanyId);
        if (selectedUserCompany == null || selectedUserCompany.Status == Status.Inactive)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);
        UserCompanyDto? userCompanyDto = _mapper.Map<UserCompanyDto>(selectedUserCompany);
        List<CompanyDto> companiesDto = _mapper.Map<List<CompanyDto>>(userCompanies.Select(uc => uc.Company));

        return new UserProfileDto() { UserCompany = userCompanyDto, Companies = companiesDto };
    }
}
