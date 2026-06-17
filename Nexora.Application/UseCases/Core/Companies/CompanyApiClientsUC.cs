using AutoMapper;

using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Core.Companies;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.Companies;

public class CompanyApiClientsUC(ICompanyRepository companyRepository, IMapper mapper) : IUseCase<CompanyApiClientsRequest, CompanyApiClientDto>
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CompanyApiClientDto> ExecuteAsync(CompanyApiClientsRequest request)
    {
        Company? company = await _companyRepository.GetByIdWithApiClientsAsync(request.CompanyId);
        if (company == null)
            throw new ApiBadRequestException(CompanyErrors.NotFound);

        var apiClients = request.ApiClientsDto
            .Select(dto =>
            {
                if (dto.Id != Guid.Empty)
                {
                    var existing = company.ApiClients.FirstOrDefault(x => x.Id == dto.Id);
                    if (existing == null)
                        throw new ApiBadRequestException(CompanyErrors.SomeApiClientWasNotExists);
                    existing.Update(dto.Name, dto.Status);
                    return existing;
                }

                return new ApiClient(
                    dto.Name,
                    dto.Status,
                    company.Id,
                    Guid.NewGuid().ToString("N"),
                    string.Empty);
            })
            .ToList();
        company.SetApiClients(apiClients);
        await _companyRepository.UpdateAsync(company);
        var updatedCompany = await _companyRepository.GetByIdWithApiClientsReadOnlyAsync(company.Id);
        return _mapper.Map<CompanyApiClientDto>(updatedCompany);
    }
}