using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects;

namespace Nexora.Application.UseCases.Culinary.Categories;

public class UpdateCategoryUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<UpdateCategoryRequest, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CategoryDto> ExecuteAsync(UpdateCategoryRequest request)
    {
        var slug = Slug.Create(request.Name.Value);
        Category? category = await _categoryRepository.GetBySlugAsync(slug, request.Context.CompanyId);
        if (category != null && category.Id != request.Id)
            throw new ApiBadRequestException(CategoryErrors.AlreadyExists);
        Category? updateCategory = await _categoryRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateCategory == null)
            throw new ApiBadRequestException(CategoryErrors.NotFound);
        updateCategory.Update(slug, request.Name, request.Description, request.ParentId, request.DisplayOrder, request.Status, request.Seo);
        await _categoryRepository.UpdateAsync(updateCategory);
        Category? updatedCategory = await _categoryRepository.GetByIdAsync(updateCategory.Id, request.Context.CompanyId);
        return _mapper.Map<CategoryDto>(updatedCategory);
    }
}

