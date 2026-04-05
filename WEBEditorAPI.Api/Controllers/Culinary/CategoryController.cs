using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Models.Culinary.Categories;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Categories;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/categories")]
public class CategoryController : ControllerBase
{
    private readonly IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>> _getAllCategoriesUC;

    public CategoryController(IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>> getAllCategoriesUC)
    {
        _getAllCategoriesUC = getAllCategoriesUC;
    }

    [Authorize(Roles = "CULINARY_CATEGORY_VIEW")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoriesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllCategoriesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, (Status?)model.Active, context);
        var result = await _getAllCategoriesUC.ExecuteAsync(request);

        return Ok(result);
    }
}
