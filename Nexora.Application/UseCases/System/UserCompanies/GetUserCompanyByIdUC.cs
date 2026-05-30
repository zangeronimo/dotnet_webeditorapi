using AutoMapper;
using Nexora.Application.DTOs.System;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.UserCompanies;

public class GetUserCompanyByIdUC(IUserCompanyRepository userCompanyRepository, IUserRepository userRepository, IMapper mapper) : IUseCase<GetByIdRequest, UserCompanyDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserCompanyDto> ExecuteAsync(GetByIdRequest request)
    {
        UserCompany? userCompany = await _userCompanyRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (userCompany == null)
            throw new ApiBadRequestException(UserCompanyErrors.NotFound);

        User? user = await _userRepository.GetByIdAsync(userCompany.UserId);
        if (user == null)
            throw new ApiNotFoundException(UserErrors.NotFound);

        var userCompanyDto = _mapper.Map<UserCompanyDto>(userCompany);
        if (string.IsNullOrEmpty(userCompanyDto.NickName))
            userCompanyDto.NickName = user.Name;

        return userCompanyDto;
    }
}