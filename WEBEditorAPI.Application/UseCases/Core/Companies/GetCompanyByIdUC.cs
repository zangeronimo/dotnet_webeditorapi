using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Companies;

public class GetCompanyByIdUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<GetByIdRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<CompanyDto> ExecuteAsync(GetByIdRequest request)
    {
        Company? company = await _companyRepository.GetByIdAsync(request.ResourceId);
        if (company == null)
            throw new ApiNotFoundException("Empresa não encontrada");
        return _mapper.Map<CompanyDto>(company);
    }
}
