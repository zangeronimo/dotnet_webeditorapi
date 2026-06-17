using AutoMapper;

using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class GetAllCompanyApiClientsUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<GetAllCompanyApiClientsRequest, CompanyApiClientDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<CompanyApiClientDto> ExecuteAsync(GetAllCompanyApiClientsRequest request)
    {
        Company? company = await _companyRepository.GetByIdWithApiClientsAsync(request.CompanyId);
        if (company == null)
            throw new ApiNotFoundException(CompanyErrors.NotFound);
        return _mapper.Map<CompanyApiClientDto>(company);
    }
}
