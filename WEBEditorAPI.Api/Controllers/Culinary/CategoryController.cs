using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Authorization;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases;

namespace WEBEditorAPI.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/categories")]
public class CategoryController : ControllerBase
{
    private readonly IUseCase<GetByIdRequest, CategoryDto> _getCategoryByIdUC;

    public CategoryController(
        IUseCase<GetByIdRequest, CategoryDto> getCategoryByIdUC)
    {
        _getCategoryByIdUC = getCategoryByIdUC;
    }

    [HasPermission("culinary.category.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var level = await _getCategoryByIdUC.ExecuteAsync(request);

        return Ok(level);
    }
}
