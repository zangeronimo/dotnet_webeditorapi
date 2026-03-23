using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Models.System;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Api.Controllers.System;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>> _getAllUsersUC;
    private readonly IUseCase<GetUserByIdRequest, UserDto> _getUserByIdUC;

    public UserController(IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>> getAllUsersUC, IUseCase<GetUserByIdRequest, UserDto> getUserByIdUC)
    {
        _getAllUsersUC = getAllUsersUC;
        _getUserByIdUC = getUserByIdUC;
    }

    [Authorize(Roles = "WEBEDITOR_USER_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserFilterModel filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        filter.CompanyId = (Guid)HttpContext.Items["CompanyId"]!;
        var result = await _getAllUsersUC.ExecuteAsync(filter);

        return Ok(result);
    }

    [Authorize(Roles = "WEBEDITOR_USER_VIEW")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var request = new GetUserByIdRequest(id, companyId);
        var user = await _getUserByIdUC.ExecuteAsync(request);

        return Ok(user);
    }
}
