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
    private readonly IUseCase<GetUserByIdModel, UserDto> _getUserByIdUC;
    private readonly IUseCase<CreateUserModel, UserDto> _createUserUC;

    public UserController(
        IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>> getAllUsersUC,
        IUseCase<GetUserByIdModel, UserDto> getUserByIdUC,
        IUseCase<CreateUserModel, UserDto> createUserUC)
    {
        _getAllUsersUC = getAllUsersUC;
        _getUserByIdUC = getUserByIdUC;
        _createUserUC = createUserUC;
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
        var request = new GetUserByIdModel(id, companyId);
        var user = await _getUserByIdUC.ExecuteAsync(request);

        return Ok(user);
    }

    [Authorize(Roles = "WEBEDITOR_USER_UPDATE")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserModel request)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        request.CompanyId = companyId;
        var user = await _createUserUC.ExecuteAsync(request);

        return Ok(user);
    }
}
