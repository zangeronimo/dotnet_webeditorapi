using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class GetCompanyByIdUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<GetByIdRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<CompanyDto> ExecuteAsync(GetByIdRequest request)
    {
        Company? company = await _companyRepository.GetByIdAsync(request.ResourceId);
        if (company == null)
            throw new ApiNotFoundException(CompanyErrors.NotFound);
        return _mapper.Map<CompanyDto>(company);
    }
}
