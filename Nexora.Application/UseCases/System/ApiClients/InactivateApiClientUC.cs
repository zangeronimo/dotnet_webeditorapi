using AutoMapper;

using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.ApiClients;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Exceptions;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.ApiClients;

public class InactivateApiClientUC(
    IApiClientRepository apiClientRepository,
    IMapper mapper) : IUseCase<InactivateApiClientRequest, ApiClientDto>
{
    private readonly IApiClientRepository _apiClientRepository = apiClientRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<ApiClientDto> ExecuteAsync(InactivateApiClientRequest request)
    {
        ApiClient? apiClient = await _apiClientRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (apiClient == null)
        {
            throw new ApiNotFoundException(ApiClientErrors.NotFound);
        }
        try
        {
            apiClient.SetInactive();
        }
        catch (DomainException e)
        {
            throw new ApiBadRequestException(e.Message);
        }
        await _apiClientRepository.UpdateAsync(apiClient);
        return _mapper.Map<ApiClientDto>(apiClient);
    }
}
