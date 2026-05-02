using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Authorization;
using WEBEditorAPI.Api.Models.Core.Modules;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases.Core.Modules;

namespace WEBEditorAPI.Api.Controllers.Core;

[ApiController]
[Route("api/core/modules")]
public class ModuleController(
    IUseCase<CreateModuleRequest, ModuleDto> createModuleUC,
    IUseCase<UpdateModuleRequest, ModuleDto> updateModuleUC) : ControllerBase
{
    private readonly IUseCase<CreateModuleRequest, ModuleDto> _createModuleUC = createModuleUC;
    private readonly IUseCase<UpdateModuleRequest, ModuleDto> _updateModuleUC = updateModuleUC;

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
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateModuleRequest(model.Id, model.Name, model.Status, model.Permissions, context);
        var module = await _updateModuleUC.ExecuteAsync(request);

        return Ok(module);
    }
}
