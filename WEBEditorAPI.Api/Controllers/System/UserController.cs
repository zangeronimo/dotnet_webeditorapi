using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Models.System;

namespace WEBEditorAPI.Api.Controllers.System;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>> _getAllUsersUC;
    private readonly IUseCase<GetUserByIdModel, UserDto> _getUserByIdUC;
    private readonly IUseCase<CreateUserModel, UserDto> _createUserUC;
    private readonly IUseCase<UpdateUserModel, UserDto> _updateUserUC;
    private readonly IUseCase<DeleteUserModel, UserDto> _deleteUserUC;

    public UserController(
        IUseCase<GetAllUserFilterModel, PaginationResult<UserDto>> getAllUsersUC,
        IUseCase<GetUserByIdModel, UserDto> getUserByIdUC,
        IUseCase<CreateUserModel, UserDto> createUserUC,
        IUseCase<UpdateUserModel, UserDto> updateUserUC,
        IUseCase<DeleteUserModel, UserDto> deleteUserUC)
    {
        _getAllUsersUC = getAllUsersUC;
        _getUserByIdUC = getUserByIdUC;
        _createUserUC = createUserUC;
        _updateUserUC = updateUserUC;
        _deleteUserUC = deleteUserUC;
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        request.CompanyId = companyId;
        var user = await _createUserUC.ExecuteAsync(request);

        return Ok(user);
    }

    [Authorize(Roles = "WEBEDITOR_USER_UPDATE")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != request.Id)
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        request.CompanyId = companyId;
        var user = await _updateUserUC.ExecuteAsync(request);

        return Ok(user);
    }

    [Authorize(Roles = "WEBEDITOR_USER_DELETE")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var request = new DeleteUserModel(id, companyId);
        var user = await _deleteUserUC.ExecuteAsync(request);

        return Ok(user);
    }
}
