using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Recipes;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Recipes;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>> _getAllRecipesUC;

    public RecipeController(
        IUseCase<GetAllRecipesFilterRequest, PaginationResult<RecipeDto>> getAllRecipesUC)
    {
        _getAllRecipesUC = getAllRecipesUC;
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
}
