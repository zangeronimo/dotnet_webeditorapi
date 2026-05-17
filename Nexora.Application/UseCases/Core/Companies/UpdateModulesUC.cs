using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class UpdateModulesUC(ICompanyRepository companyRepository, IModuleRepository moduleRepository, IMapper mapper) : IUseCase<UpdateModulesRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CompanyDto> ExecuteAsync(UpdateModulesRequest request)
    {
        Company? company = await _companyRepository.GetByIdAsync(request.CompanyId);
        if (company == null)
            throw new ApiBadRequestException("Empresa não encontrada.");
        var modules = await _moduleRepository.GetByRangeIdAsync(request.ModuleIds);
        if (modules.Count != request.ModuleIds.Count)
        {
            throw new ApiBadRequestException("Algum módulo não existe");
        }
        company.SetModules(modules);
        await _companyRepository.UpdateAsync(company);
        var updatedCompany = await _companyRepository.GetByIdReadOnlyAsync(company.Id);
        return _mapper.Map<CompanyDto>(updatedCompany);
    }
}