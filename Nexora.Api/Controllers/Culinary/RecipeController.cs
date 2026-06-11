using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.Culinary.Recipes;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Culinary.Recipes;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>> _getAllRecipesUC;
    private readonly IUseCase<GetByIdRequest, RecipeDto> _getRecipeByIdUC;
    private readonly IUseCase<CreateRecipeRequest, RecipeDto> _createRecipeUC;
    private readonly IUseCase<UpdateRecipeRequest, RecipeDto> _updateRecipeUC;
    private readonly IUseCase<DeleteRequest, RecipeDto> _deleteRecipeUC;
    private readonly IUseCase<RecipeImageUploadRequest, RecipeDto> _recipeImageUploadUC;

    public RecipeController(
        IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>> getAllRecipesUC,
        IUseCase<GetByIdRequest, RecipeDto> getRecipeByIdUC,
        IUseCase<CreateRecipeRequest, RecipeDto> createRecipeUC,
        IUseCase<UpdateRecipeRequest, RecipeDto> updateRecipeUC,
        IUseCase<DeleteRequest, RecipeDto> deleteRecipeUC,
        IUseCase<RecipeImageUploadRequest, RecipeDto> recipeImageUploadUC)
    {
        _getAllRecipesUC = getAllRecipesUC;
        _getRecipeByIdUC = getRecipeByIdUC;
        _createRecipeUC = createRecipeUC;
        _updateRecipeUC = updateRecipeUC;
        _deleteRecipeUC = deleteRecipeUC;
        _recipeImageUploadUC = recipeImageUploadUC;
    }

    [HasPermission("culinary.recipe.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRecipesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllRecipesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, model.CategoryId, context);
        var result = await _getAllRecipesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("culinary.recipe.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var recipe = await _getRecipeByIdUC.ExecuteAsync(request);

        return Ok(recipe);
    }

    [HasPermission("culinary.recipe.create")]
    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var recipeContent = new RecipeContent(model.ShortDescription, model.FullDescription, model.Sections, model.Notes);
        var recipeTiming = new RecipeTiming(model.PrepTime, model.CookTime, model.RestTime);
        var recipeYield = new RecipeYield(model.YieldTotal);
        var RecipeAttributes = new RecipeAttributes(model.difficulty, model.Cuisine);
        var recipeSeo = new RecipeSeo(model.MetaTitle, model.MetaDescription, model.CanonicalUrl);
        var request = new CreateRecipeRequest(
            model.Name,
            recipeContent,
            recipeTiming,
            recipeYield,
            RecipeAttributes,
            recipeSeo,
            model.TagIds,
            model.Status,
            model.CategoryId,
             context);
        var recipe = await _createRecipeUC.ExecuteAsync(request);

        return Ok(recipe);
    }

    [HasPermission("culinary.recipe.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException(ControllerErrors.RouteIdBodyId);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var recipeContent = new RecipeContent(model.ShortDescription, model.FullDescription, model.Sections, model.Notes);
        var recipeTiming = new RecipeTiming(model.PrepTime, model.CookTime, model.RestTime);
        var recipeYield = new RecipeYield(model.YieldTotal);
        var RecipeAttributes = new RecipeAttributes(model.difficulty, model.Cuisine);
        var recipeSeo = new RecipeSeo(model.MetaTitle, model.MetaDescription, model.CanonicalUrl);
        var request = new UpdateRecipeRequest(
            model.Id,
            model.Name,
            recipeContent,
            recipeTiming,
            recipeYield,
            RecipeAttributes,
            recipeSeo,
            model.TagIds,
            model.Status,
            model.CategoryId,
             context);
        var recipe = await _updateRecipeUC.ExecuteAsync(request);

        return Ok(recipe);
    }

    [HasPermission("culinary.recipe.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var recipe = await _deleteRecipeUC.ExecuteAsync(request);

        return Ok(recipe);
    }

    [HasPermission("culinary.recipe.update")]
    [HttpPut("{id}/image")]
    public async Task<IActionResult> ImageUpload([FromRoute] Guid id, [FromForm] RecipeImageUploadModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        FileData fileData = new FileData(model.Image.OpenReadStream(), model.Image.FileName, model.Image.ContentType, model.Image.Length);
        var request = new RecipeImageUploadRequest(id, fileData, context);
        var recipe = await _recipeImageUploadUC.ExecuteAsync(request);

        return Ok(recipe);
    }
}
