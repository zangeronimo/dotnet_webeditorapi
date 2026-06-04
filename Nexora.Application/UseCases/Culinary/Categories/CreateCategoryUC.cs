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

public class CreateCategoryUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<CreateCategoryRequest, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<CategoryDto> ExecuteAsync(CreateCategoryRequest request)
    {
        var slug = Slug.Create(request.Name.Value);
        Category? category = await _categoryRepository.GetBySlugAsync(slug, request.Context.CompanyId);
        if (category != null)
            throw new ApiBadRequestException(CategoryErrors.AlreadyExists);
        Category newCategory = new Category(slug, request.Name, request.Description, request.ParentId, request.DisplayOrder, request.Status, request.Seo, request.Context.CompanyId);
        await _categoryRepository.AddAsync(newCategory);
        Category? createdCategory = await _categoryRepository.GetByIdAsync(newCategory.Id, request.Context.CompanyId);
        return _mapper.Map<CategoryDto>(createdCategory);
    }
}
