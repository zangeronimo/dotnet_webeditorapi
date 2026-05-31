using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class CreateCompanyUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<CreateCompanyRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CompanyDto> ExecuteAsync(CreateCompanyRequest request)
    {
        Company? company = await _companyRepository.GetByNameAsync(request.Name);
        if (company != null)
            throw new ApiBadRequestException(CompanyErrors.AlreadyExists);
        Company newCompany = new Company(request.Name, request.Status);
        await _companyRepository.AddAsync(newCompany);
        Company? createdCompany = await _companyRepository.GetByIdAsync(newCompany.Id);
        return _mapper.Map<CompanyDto>(createdCompany);
    }
}
