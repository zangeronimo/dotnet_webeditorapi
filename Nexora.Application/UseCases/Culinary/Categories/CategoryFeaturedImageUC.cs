using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Categories;

public class CategoryFeaturedImageUC(ICategoryRepository categoryRepository, IStorageProvider storageProvider, IMapper mapper) : IUseCase<CategoryFeaturedImageRequest, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IStorageProvider _storageProvider = storageProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<CategoryDto> ExecuteAsync(CategoryFeaturedImageRequest request)
    {
        Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId, request.Context.CompanyId);
        if (category == null)
            throw new ApiNotFoundException(CategoryErrors.NotFound);
        var featuredImageUrl = await _storageProvider.SaveStreamAsync(request.FeaturedImage, request.Context.CompanyId.ToString(), "culinary/category");
        category.SetFeaturedImageUrl(featuredImageUrl);
        await _categoryRepository.UpdateAsync(category);
        Category? updatedCategory = await _categoryRepository.GetByIdAsync(category.Id, request.Context.CompanyId);
        return _mapper.Map<CategoryDto>(updatedCategory);
    }
}
