using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class DeleteCompanyUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<DeleteRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CompanyDto> ExecuteAsync(DeleteRequest request)
    {
        Company? company = await _companyRepository.GetByIdAsync(request.ResourceId);
        if (company == null)
            throw new ApiNotFoundException("Empresa não encontrada");
        company.Delete();
        await _companyRepository.UpdateAsync(company);
        return _mapper.Map<CompanyDto>(company);
    }
}

