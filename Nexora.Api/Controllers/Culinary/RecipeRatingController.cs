using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.Culinary.RecipeRatings;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Culinary.RecipeRatings;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/reciperatings")]
public class RecipeRatingController : ControllerBase
{
    private readonly IUseCase<GetAllRecipeRatingsFilterRequest, PaginationResult<RecipeRatingDto>> _getAllRecipeRatingsUC;
    private readonly IUseCase<GetByIdRequest, RecipeRatingDto> _getRecipeRatingByIdUC;
    private readonly IUseCase<UpdateRecipeRatingRequest, RecipeRatingDto> _updateRecipeRatingUC;
    private readonly IUseCase<DeleteRequest, RecipeRatingDto> _deleteRecipeRatingUC;

    public RecipeRatingController(
        IUseCase<GetAllRecipeRatingsFilterRequest, PaginationResult<RecipeRatingDto>> getAllRecipeRatingsUC,
        IUseCase<GetByIdRequest, RecipeRatingDto> getRecipeRatingByIdUC,
        IUseCase<UpdateRecipeRatingRequest, RecipeRatingDto> updateRecipeRatingUC,
        IUseCase<DeleteRequest, RecipeRatingDto> deleteRecipeRatingUC)
    {
        _getAllRecipeRatingsUC = getAllRecipeRatingsUC;
        _getRecipeRatingByIdUC = getRecipeRatingByIdUC;
        _updateRecipeRatingUC = updateRecipeRatingUC;
        _deleteRecipeRatingUC = deleteRecipeRatingUC;
    }

    [HasPermission("culinary.reciperating.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRecipeRatingsFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new Application.Requests.RequestContext(userId, companyId);
        var request = new GetAllRecipeRatingsFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, context);
        var result = await _getAllRecipeRatingsUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("culinary.reciperating.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var reciperating = await _getRecipeRatingByIdUC.ExecuteAsync(request);

        return Ok(reciperating);
    }

    [HasPermission("culinary.reciperating.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipeRating([FromBody] UpdateRecipeRatingModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException(ControllerErrors.RouteIdBodyId);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var recipeScore = new RecipeScore(model.Score);
        var request = new UpdateRecipeRatingRequest(id, recipeScore, model.Name, model.Comment, model.Status, model.RecipeId, context);
        var reciperating = await _updateRecipeRatingUC.ExecuteAsync(request);

        return Ok(reciperating);
    }

    [HasPermission("culinary.reciperating.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var reciperating = await _deleteRecipeRatingUC.ExecuteAsync(request);

        return Ok(reciperating);
    }
}

