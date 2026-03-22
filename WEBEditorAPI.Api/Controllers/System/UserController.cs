using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Models.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;

namespace WEBEditorAPI.Api.Controllers.System;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Authorize(Roles = "WEBEDITOR_USER_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserFilterModel filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var users = await _userRepository.GetAllAsync(companyId);

        return Ok(users);
    }
}
