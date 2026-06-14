using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.Culinary.Categories;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/categories")]
public class CategoryController : ControllerBase
{
    private readonly IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>> _getAllCategoriesUC;
    private readonly IUseCase<GetByParentIdRequest, IEnumerable<CategoryDto>> _getByParentIdUC;
    private readonly IUseCase<GetAllParentsRequest, IEnumerable<CategoryDto>> _getAllParentsUC;
    private readonly IUseCase<GetByIdRequest, CategoryDto> _getCategoryByIdUC;
    private readonly IUseCase<CreateCategoryRequest, CategoryDto> _createCategoryUC;
    private readonly IUseCase<UpdateCategoryRequest, CategoryDto> _updateCategoryUC;
    private readonly IUseCase<DeleteRequest, CategoryDto> _deleteCategoryUC;
    private readonly IUseCase<CategoryFeaturedImageRequest, CategoryDto> _categoryFeaturedImageUC;

    public CategoryController(
        IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>> getAllCategoriesUC,
        IUseCase<GetByParentIdRequest, IEnumerable<CategoryDto>> getByParentIdUC,
        IUseCase<GetAllParentsRequest, IEnumerable<CategoryDto>> getAllParentsUC,
        IUseCase<GetByIdRequest, CategoryDto> getCategoryByIdUC,
        IUseCase<CreateCategoryRequest, CategoryDto> createCategoryUC,
        IUseCase<UpdateCategoryRequest, CategoryDto> updateCategoryUC,
        IUseCase<DeleteRequest, CategoryDto> deleteCategoryUC,
        IUseCase<CategoryFeaturedImageRequest, CategoryDto> categoryFeaturedImageUC)
    {
        _getAllCategoriesUC = getAllCategoriesUC;
        _getAllParentsUC = getAllParentsUC;
        _getByParentIdUC = getByParentIdUC;
        _getCategoryByIdUC = getCategoryByIdUC;
        _createCategoryUC = createCategoryUC;
        _updateCategoryUC = updateCategoryUC;
        _deleteCategoryUC = deleteCategoryUC;
        _categoryFeaturedImageUC = categoryFeaturedImageUC;
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
        var request = new GetAllCategoriesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, model.Parent, context);
        var result = await _getAllCategoriesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("culinary.category.view")]
    [HttpGet("children")]
    public async Task<IActionResult> GetAllChildren([FromQuery] GetByParentIdModel model)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByParentIdRequest(model.ParentId, context);
        var result = await _getByParentIdUC.ExecuteAsync(request);

        return Ok(result);
    }


    [HasPermission("culinary.category.view")]
    [HttpGet("parents")]
    public async Task<IActionResult> GetAllParents()
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllParentsRequest(context);
        var result = await _getAllParentsUC.ExecuteAsync(request);

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
        var category = await _getCategoryByIdUC.ExecuteAsync(request);

        return Ok(category);
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
        var category = await _createCategoryUC.ExecuteAsync(request);

        return Ok(category);
    }

    [HasPermission("culinary.category.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException(ControllerErrors.RouteIdBodyId);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var categoryName = new CategoryName(model.Name);
        var categorySeo = new CategorySeo(model.MetaTitle, model.MetaDescription, model.CanonicalUrl);
        var request = new UpdateCategoryRequest(id, categoryName, model.Description, model.ParentId, model.DisplayOrder, model.Status, categorySeo, context);
        var category = await _updateCategoryUC.ExecuteAsync(request);

        return Ok(category);
    }

    [HasPermission("culinary.category.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var category = await _deleteCategoryUC.ExecuteAsync(request);

        return Ok(category);
    }

    [HasPermission("culinary.category.update")]
    [HttpPut("{id}/featured_image")]
    public async Task<IActionResult> FeaturedImage([FromRoute] Guid id, [FromForm] CategoryFeaturedImageModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        FileData fileData = new FileData(model.FeaturedImage.OpenReadStream(), model.FeaturedImage.FileName, model.FeaturedImage.ContentType, model.FeaturedImage.Length);
        var request = new CategoryFeaturedImageRequest(id, fileData, context);
        var category = await _categoryFeaturedImageUC.ExecuteAsync(request);

        return Ok(category);
    }
}
