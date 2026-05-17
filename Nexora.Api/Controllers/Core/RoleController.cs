using Microsoft.AspNetCore.Mvc;
using Nexora.Api.Authorization;
using Nexora.Api.Models.Core.Roles;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Core.Roles;
using Nexora.Application.UseCases.Core.Roles;

namespace Nexora.Api.Controllers.Core;

[ApiController]
[Route("api/core/roles")]
public class RoleController(
    IUseCase<GetAllRolesFilterRequest, PaginationResult<RoleDto>> getAllRolesUC,
    IUseCase<GetByIdRequest, RoleDto> getRoleByIdUC,
    IUseCase<CreateRoleRequest, RoleDto> createRoleUC,
    IUseCase<UpdateRoleRequest, RoleDto> updateRoleUC,
    IUseCase<UpdatePermissionsRequest, RoleDto> updatePermissionsUC,
    IUseCase<DeleteRequest, RoleDto> deleteRoleUC) : ControllerBase
{
    private readonly IUseCase<GetAllRolesFilterRequest, PaginationResult<RoleDto>> _getAllRolesUC = getAllRolesUC;
    private readonly IUseCase<GetByIdRequest, RoleDto> _getRoleByIdUC = getRoleByIdUC;
    private readonly IUseCase<CreateRoleRequest, RoleDto> _createRoleUC = createRoleUC;
    private readonly IUseCase<UpdateRoleRequest, RoleDto> _updateRoleUC = updateRoleUC;
    private readonly IUseCase<UpdatePermissionsRequest, RoleDto> _updatePermissionsUC = updatePermissionsUC;
    private readonly IUseCase<DeleteRequest, RoleDto> _deleteRoleUC = deleteRoleUC;

    [HasPermission("core.role.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRolesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllRolesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, context);
        var result = await _getAllRolesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("core.role.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var role = await _getRoleByIdUC.ExecuteAsync(request);

        return Ok(role);
    }

    [HasPermission("core.role.create")]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateRoleRequest(model.Name, model.Status, context);
        var role = await _createRoleUC.ExecuteAsync(request);

        return Ok(role);
    }

    [HasPermission("core.role.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateRoleRequest(id, model.Name, model.Status, context);
        var role = await _updateRoleUC.ExecuteAsync(request);

        return Ok(role);
    }

    [HasPermission("core.role.update")]
    [HttpPut("{id}/permissions")]
    public async Task<IActionResult> UpdatePermissions([FromBody] UpdatePermissionsModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdatePermissionsRequest(id, model.Permissions, context);
        var company = await _updatePermissionsUC.ExecuteAsync(request);

        return Ok(company);
    }

    [HasPermission("core.role.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var role = await _deleteRoleUC.ExecuteAsync(request);

        return Ok(role);
    }
}