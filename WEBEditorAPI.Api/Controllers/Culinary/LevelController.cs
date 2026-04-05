using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Levels;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/levels")]
public class LevelController : ControllerBase
{
    private readonly IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>> _getAllLevelsUC;
    private readonly IUseCase<GetByIdRequest, LevelDto> _getLevelByIdUC;

    public LevelController(
        IUseCase<GetAllLevelsFilterRequest, PaginationResult<LevelDto>> getAllLevelsUC,
        IUseCase<GetByIdRequest, LevelDto> getLevelByIdUC)
    {
        _getAllLevelsUC = getAllLevelsUC;
        _getLevelByIdUC = getLevelByIdUC;
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

    [Authorize(Roles = "CULINARY_LEVEL_VIEW")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var level = await _getLevelByIdUC.ExecuteAsync(request);

        return Ok(level);
    }
}
