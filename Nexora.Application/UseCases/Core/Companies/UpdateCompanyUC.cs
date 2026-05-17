using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class UpdateCompanyUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<UpdateCompanyRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CompanyDto> ExecuteAsync(UpdateCompanyRequest request)
    {
        Company? company = await _companyRepository.GetByNameAsync(request.Name);
        if (company != null && company.Id != request.Id)
            throw new ApiBadRequestException("Empresa já cadastrada com esse nome.");
        Company? updateCompany = await _companyRepository.GetByIdAsync(request.Id);
        if (updateCompany == null)
            throw new ApiBadRequestException("Empresa não encontrada.");
        updateCompany.Update(request.Name, request.Active);
        await _companyRepository.UpdateAsync(updateCompany);
        Company? updatedCompany = await _companyRepository.GetByIdReadOnlyAsync(updateCompany.Id);
        return _mapper.Map<CompanyDto>(updatedCompany);
    }
}