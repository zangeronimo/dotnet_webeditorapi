using AutoMapper;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.System;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.UserCompanies;
using Nexora.Domain.Entities.System;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.UserCompanies;

public class GetAllUserCompaniesUC(IUserCompanyRepository userCompanyRepository, IMapper mapper) : IUseCase<GetAllUserCompaniesFilterRequest, PaginationResult<UserCompanyDto>>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<UserCompanyDto>> ExecuteAsync(GetAllUserCompaniesFilterRequest request)
    {
        (IEnumerable<UserCompany> userCompanies, int total) = await _userCompanyRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.NickName, request.Status, request.Context.CompanyId);

        return new PaginationResult<UserCompanyDto>
        {
            Items = _mapper.Map<IEnumerable<UserCompanyDto>>(userCompanies),
            Total = total
        };
    }
}

