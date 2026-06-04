using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.Culinary.Categories;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/categories")]
public class CategoryController : ControllerBase
{
    private readonly IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>> _getAllCategoriesUC;
    private readonly IUseCase<GetByIdRequest, CategoryDto> _getCategoryByIdUC;
    private readonly IUseCase<CreateCategoryRequest, CategoryDto> _createCategoryUC;

    public CategoryController(
        IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>> getAllCategoriesUC,
        IUseCase<GetByIdRequest, CategoryDto> getCategoryByIdUC,
        IUseCase<CreateCategoryRequest, CategoryDto> createCategoryUC)
    {
        _getAllCategoriesUC = getAllCategoriesUC;
        _getCategoryByIdUC = getCategoryByIdUC;
        _createCategoryUC = createCategoryUC;
    }

    [HasPermission("culinary.category.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoriesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllCategoriesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, context);
        var result = await _getAllCategoriesUC.ExecuteAsync(request);

        return Ok(result);
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

    [HasPermission("culinary.category.create")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var categoryName = new CategoryName(model.Name);
        var categorySeo = new CategorySeo(model.MetaTitle, model.MetaDescription, model.CanonicalUrl);
        var request = new CreateCategoryRequest(categoryName, model.Description, model.ParentId, model.DisplayOrder, model.Status, categorySeo, context);
        var role = await _createCategoryUC.ExecuteAsync(request);

        return Ok(role);
    }
}
