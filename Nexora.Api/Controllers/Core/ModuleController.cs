using Microsoft.AspNetCore.Mvc;
using Nexora.Api.Authorization;
using Nexora.Api.Models.Core.Modules;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Core.Modules;
using Nexora.Domain.Errors.Core;

namespace Nexora.Api.Controllers.Core;

[ApiController]
[Route("api/core/modules")]
public class ModuleController(
    IUseCase<GetAllModulesFilterRequest, PaginationResult<ModuleDto>> getAllModulesUC,
    IUseCase<GetByIdRequest, ModuleDto> getModuleByIdUC,
    IUseCase<CreateModuleRequest, ModuleDto> createModuleUC,
    IUseCase<UpdateModuleRequest, ModuleDto> updateModuleUC,
    IUseCase<DeleteRequest, ModuleDto> deleteModuleUC) : ControllerBase
{
    private readonly IUseCase<GetAllModulesFilterRequest, PaginationResult<ModuleDto>> _getAllModulesUC = getAllModulesUC;
    private readonly IUseCase<GetByIdRequest, ModuleDto> _getModuleByIdUC = getModuleByIdUC;
    private readonly IUseCase<CreateModuleRequest, ModuleDto> _createModuleUC = createModuleUC;
    private readonly IUseCase<UpdateModuleRequest, ModuleDto> _updateModuleUC = updateModuleUC;
    private readonly IUseCase<DeleteRequest, ModuleDto> _deleteModuleUC = deleteModuleUC;

    [HasPermission("core.module.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllModulesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllModulesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, context);
        var result = await _getAllModulesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("core.module.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var module = await _getModuleByIdUC.ExecuteAsync(request);

        return Ok(module);
    }

    [HasPermission("core.module.create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModuleModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateModuleRequest(model.Name, model.Status, context);
        var module = await _createModuleUC.ExecuteAsync(request);

        return Ok(module);
    }

    [HasPermission("core.module.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateModuleModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException(ControllerErrors.RouteIdBodyId);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateModuleRequest(model.Id, model.Name, model.Status, model.Permissions, context);
        var module = await _updateModuleUC.ExecuteAsync(request);

        return Ok(module);
    }

    [HasPermission("core.module.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var module = await _deleteModuleUC.ExecuteAsync(request);

        return Ok(module);
    }
}
