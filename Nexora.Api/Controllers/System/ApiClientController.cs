using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.System.Roles;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases.System.ApiClients;
using Nexora.Domain.Errors.Core;

namespace Nexora.Api.Controllers.System;

[ApiController]
[Route("api/system/api-clients")]
public class ApiClientController(
    IUseCase<GetAllApiClientsRequest, IEnumerable<ApiClientDto>> getAllApiClientsUC,
    IUseCase<GenerateApiClientRequest, GenerateApiClientSecretDto> generateApiClientUC) : ControllerBase
{
    private readonly IUseCase<GetAllApiClientsRequest, IEnumerable<ApiClientDto>> _getAllApiClientsUC = getAllApiClientsUC;
    private readonly IUseCase<GenerateApiClientRequest, GenerateApiClientSecretDto> _generateApiClientUC = generateApiClientUC;

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

    [HasPermission("system.apiclient.update")]
    [HttpPut("{id}/generate")]
    public async Task<IActionResult> Generate([FromBody] GenerateApiClientModel model, [FromRoute] Guid id)
    {
        if (id != model.Id)
            throw new ApiBadRequestException(ControllerErrors.RouteIdBodyId);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GenerateApiClientRequest(model.Id, context);
        var role = await _generateApiClientUC.ExecuteAsync(request);

        return Ok(role);
    }
}