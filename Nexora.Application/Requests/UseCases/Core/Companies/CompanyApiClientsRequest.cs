using Nexora.Application.DTOs.Core;

namespace Nexora.Application.Requests.UseCases.Core.Companies;

public sealed record CompanyApiClientsRequest(
    Guid CompanyId,
    List<ApiClientDto> ApiClientsDto,
    RequestContext Context
) : ApplicationRequest(Context);
