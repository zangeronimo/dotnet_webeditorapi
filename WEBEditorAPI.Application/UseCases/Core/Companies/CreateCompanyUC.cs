using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.Companies;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.Companies;

public class CreateCompanyUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<CreateCompanyRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CompanyDto> ExecuteAsync(CreateCompanyRequest request)
    {
        Company? company = await _companyRepository.GetByNameAsync(request.Name);
        if (company != null)
            throw new ApiBadRequestException("Empresa já cadastrada com esse nome");
        Company newCompany = new Company(request.Name, request.Status);
        await _companyRepository.AddAsync(newCompany);
        Company? createdCompany = await _companyRepository.GetByIdAsync(newCompany.Id);
        return _mapper.Map<CompanyDto>(createdCompany);
    }
}
