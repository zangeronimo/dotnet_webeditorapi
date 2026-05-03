using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Companies;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Companies;

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
