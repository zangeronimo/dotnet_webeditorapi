using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Ratings;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
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
    private readonly IUseCase<GetAllRatingsFilterRequest, PaginationResult<RatingDto>> _getAllUC;
    private readonly IUseCase<GetByIdRequest, RatingDto> _getByIdUC;
    private readonly IUseCase<UpdateRatingRequest, RatingDto> _updateUC;

    public RatingController(
        IUseCase<GetAllRatingsFilterRequest, PaginationResult<RatingDto>> getAllUC,
        IUseCase<GetByIdRequest, RatingDto> getByIdUC,
        IUseCase<UpdateRatingRequest, RatingDto> updateUC)
    {
        _getAllUC = getAllUC;
        _getByIdUC = getByIdUC;
        _updateUC = updateUC;
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
        var result = await _getAllUC.ExecuteAsync(request);

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

    [Authorize(Roles = "CULINARY_RATING_UPDATE")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRatingModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateRatingRequest(model.Id, model.Rate, model.Name, model.Comment, model.Active, context);
        var level = await _updateUC.ExecuteAsync(request);

        return Ok(level);
    }
}
