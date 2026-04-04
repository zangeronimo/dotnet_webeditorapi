using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.System.Users;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases.System.Users;

namespace WEBEditorAPI.Api.Controllers.System;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>> _getAllUsersUC;
    private readonly IUseCase<GetUserByIdRequest, UserDto> _getUserByIdUC;
    private readonly IUseCase<CreateUserRequest, UserDto> _createUserUC;
    private readonly IUseCase<UpdateUserRequest, UserDto> _updateUserUC;
    private readonly IUseCase<DeleteUserRequest, UserDto> _deleteUserUC;

    public UserController(
        IUseCase<GetAllUsersFilterRequest, PaginationResult<UserDto>> getAllUsersUC,
        IUseCase<GetUserByIdRequest, UserDto> getUserByIdUC,
        IUseCase<CreateUserRequest, UserDto> createUserUC,
        IUseCase<UpdateUserRequest, UserDto> updateUserUC,
        IUseCase<DeleteUserRequest, UserDto> deleteUserUC)
    {
        _getAllUsersUC = getAllUsersUC;
        _getUserByIdUC = getUserByIdUC;
        _createUserUC = createUserUC;
        _updateUserUC = updateUserUC;
        _deleteUserUC = deleteUserUC;
    }

    [Authorize(Roles = "WEBEDITOR_USER_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllUsersFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Email, context);
        var result = await _getAllUsersUC.ExecuteAsync(request);

        return Ok(result);
    }

    [Authorize(Roles = "WEBEDITOR_USER_VIEW")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetUserByIdRequest(id, context);
        var user = await _getUserByIdUC.ExecuteAsync(request);

        return Ok(user);
    }

    [Authorize(Roles = "WEBEDITOR_USER_UPDATE")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateUserRequest(model.Name, model.Email, model.Password, context);
        var user = await _createUserUC.ExecuteAsync(request);

        return Ok(user);
    }

    [Authorize(Roles = "WEBEDITOR_USER_UPDATE")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateUserRequest(model.Id, model.Name, model.Email, model.Password, model.RoleIds, context);
        var user = await _updateUserUC.ExecuteAsync(request);

        return Ok(user);
    }

    [Authorize(Roles = "WEBEDITOR_USER_DELETE")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteUserRequest(id, context);
        var user = await _deleteUserUC.ExecuteAsync(request);

        return Ok(user);
    }
}
