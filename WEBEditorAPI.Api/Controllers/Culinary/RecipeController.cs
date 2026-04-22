using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Recipes;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>> _getAllRecipesUC;
    private readonly IUseCase<GetByIdRequest, RecipeDto> _getRecipeByIdUC;
    private readonly IUseCase<CreateRecipeRequest, RecipeDto> _createRecipeUC;
    private readonly IUseCase<UpdateRecipeRequest, RecipeDto> _updateRecipeUC;

    public RecipeController(
        IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>> getAllRecipesUC,
        IUseCase<GetByIdRequest, RecipeDto> getRecipeByIdUC,
        IUseCase<CreateRecipeRequest, RecipeDto> createRecipeUC,
        IUseCase<UpdateRecipeRequest, RecipeDto> updateRecipeUC)
    {
        _getAllRecipesUC = getAllRecipesUC;
        _getRecipeByIdUC = getRecipeByIdUC;
        _createRecipeUC = createRecipeUC;
        _updateRecipeUC = updateRecipeUC;
    }

    [Authorize(Roles = "CULINARY_RECIPE_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRecipesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllRecipesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, (Status?)model.Active, context);
        var result = await _getAllRecipesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [Authorize(Roles = "CULINARY_RECIPE_VIEW")]
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

    [Authorize(Roles = "CULINARY_RECIPE_UPDATE")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecipeModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateRecipeRequest(
            model.Name,
            new RecipeContent(model.ShortDescription, model.FullDescription, model.Ingredients, model.Preparation, model.Notes),
            new RecipeTiming(model.PrepTime, model.CookTime, model.RestTime),
            new RecipeYield(model.YieldTotal),
            new RecipeAttributes(model.Difficulty, model.Tools, model.Cuisine),
            new RecipeSeo(model.MetaTitle, model.MetaDescription, model.Keywords.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()),
            model.LevelId,
            context);
        var recipe = await _createRecipeUC.ExecuteAsync(request);

        return Ok(recipe);
    }

    [Authorize(Roles = "CULINARY_RECIPE_UPDATE")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRecipeModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateRecipeRequest(
            model.Id,
            model.Slug,
            model.Name,
            new RecipeContent(model.ShortDescription, model.FullDescription, model.Ingredients, model.Preparation, model.Notes),
            new RecipeTiming(model.PrepTime, model.CookTime, model.RestTime),
            new RecipeYield(model.YieldTotal),
            new RecipeAttributes(model.Difficulty, model.Tools, model.Cuisine),
            new RecipeSeo(model.MetaTitle, model.MetaDescription, model.Keywords.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()),
            model.LevelId,
            model.Active,
            model.Image,
            context);
        var recipe = await _updateRecipeUC.ExecuteAsync(request);

        return Ok(recipe);
    }
}
