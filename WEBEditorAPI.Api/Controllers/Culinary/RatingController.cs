using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Ratings;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Ratings;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/ratings")]
public class RatingController : ControllerBase
{
    private readonly IUseCase<GetAllRatingsFilterRequest, PaginationResult<RatingDto>> _getAllRatingUC;
    private readonly IUseCase<GetByIdRequest, RatingDto> _getByIdUC;

    public RatingController(
        IUseCase<GetAllRatingsFilterRequest, PaginationResult<RatingDto>> getAllRatingUC,
        IUseCase<GetByIdRequest, RatingDto> getByIdUC)
    {
        _getAllRatingUC = getAllRatingUC;
        _getByIdUC = getByIdUC;
    }

    [Authorize(Roles = "CULINARY_RATING_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRatingsFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllRatingsFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, (Status?)model.Active, context);
        var result = await _getAllRatingUC.ExecuteAsync(request);

        return Ok(result);
    }

    [Authorize(Roles = "CULINARY_RATING_VIEW")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var level = await _getByIdUC.ExecuteAsync(request);

        return Ok(level);
    }
}
