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

    public UserController(IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>> getAllUsersUC)
    {
        _getAllUsersUC = getAllUsersUC;
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
}
