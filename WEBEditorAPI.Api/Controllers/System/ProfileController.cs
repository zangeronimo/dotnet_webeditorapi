
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;

namespace WEBEditorAPI.Api.Controllers.System;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly IUseCase<GetByIdRequest, UserDto> _getUserByIdUC;

    public ProfileController(IUseCase<GetByIdRequest, UserDto> getUserByIdUC)
    {
        _getUserByIdUC = getUserByIdUC;
    }

    [HttpGet]
    public async Task<IActionResult> GetById()
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new Application.Requests.RequestContext(userId, companyId);
        var request = new GetByIdRequest(userId, context);
        var user = await _getUserByIdUC.ExecuteAsync(request);

        return Ok(user);
    }

}

