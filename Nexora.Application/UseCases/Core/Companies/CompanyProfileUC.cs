using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class CompanyProfileUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<CompanyProfileRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CompanyDto> ExecuteAsync(CompanyProfileRequest request)
    {
        Company? company = await _companyRepository.GetByNameAsync(request.Name);
        if (company != null && company.Id != request.Context.CompanyId)
            throw new ApiBadRequestException(CompanyErrors.AlreadyExists);
        Company? updateCompany = await _companyRepository.GetByIdAsync(request.Context.CompanyId);
        if (updateCompany == null || updateCompany.Status == Status.Inactive)
            throw new ApiBadRequestException(CompanyErrors.NotFound);
        updateCompany.Update(request.Name, Status.Active);
        await _companyRepository.UpdateAsync(updateCompany);
        Company? updatedCompany = await _companyRepository.GetByIdReadOnlyAsync(updateCompany.Id);
        return _mapper.Map<CompanyDto>(updatedCompany);
    }
}
