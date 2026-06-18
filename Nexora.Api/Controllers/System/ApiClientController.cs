using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases.System.ApiClients;

namespace Nexora.Api.Controllers.System;

[ApiController]
[Route("api/system/api-clients")]
public class ApiClientController(
    IUseCase<GetAllApiClientsRequest, IEnumerable<ApiClientDto>> getAllApiClientsUC) : ControllerBase
{
    private readonly IUseCase<GetAllApiClientsRequest, IEnumerable<ApiClientDto>> _getAllApiClientsUC = getAllApiClientsUC;

    [HasPermission("system.apiclient.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllApiClientsRequest(context);
        var result = await _getAllApiClientsUC.ExecuteAsync(request);

        return Ok(result);
    }
}