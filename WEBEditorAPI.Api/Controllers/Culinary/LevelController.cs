using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Levels;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/levels")]
public class LevelController : ControllerBase
{
    private readonly IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>> _getAllLevelsUC;

    public LevelController(IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>> getAllLevelsUC)
    {
        _getAllLevelsUC = getAllLevelsUC;
    }

    [Authorize(Roles = "CULINARY_LEVEL_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllLevelsFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllLevelsFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, (Status?)model.Active, context);
        var result = await _getAllLevelsUC.ExecuteAsync(request);

        return Ok(result);
    }
}
