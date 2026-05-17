using AutoMapper;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class GetAllCompaniesUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<GetAllCompaniesFilterRequest, PaginationResult<CompanyDto>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<CompanyDto>> ExecuteAsync(GetAllCompaniesFilterRequest request)
    {
        (IEnumerable<Company> companies, int total) = await _companyRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status);

        return new PaginationResult<CompanyDto>
        {
            Items = _mapper.Map<IEnumerable<CompanyDto>>(companies),
            Total = total
        };
    }
}
