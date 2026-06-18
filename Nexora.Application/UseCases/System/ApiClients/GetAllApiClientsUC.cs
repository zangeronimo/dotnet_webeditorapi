using AutoMapper;

using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.ApiClients;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.ApiClients;

public class GetAllApiClientsUC(IApiClientRepository apiClientRepository, IMapper mapper) : IUseCase<GetAllApiClientsRequest, IEnumerable<ApiClientDto>>
{
    private readonly IApiClientRepository _apiClientRepository = apiClientRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ApiClientDto>> ExecuteAsync(GetAllApiClientsRequest request)
    {
        IEnumerable<ApiClient> apiClients = await _apiClientRepository.GetAllAsync(request.Context.CompanyId);

        return _mapper.Map<IEnumerable<ApiClientDto>>(apiClients);
    }
}
