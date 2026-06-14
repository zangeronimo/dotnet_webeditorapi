using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Categories;

public class DeleteCategoryUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<DeleteRequest, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CategoryDto> ExecuteAsync(DeleteRequest request)
    {
        Category? category = await _categoryRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (category == null)
            throw new ApiNotFoundException(CategoryErrors.NotFound);
        var categories = await _categoryRepository.GetAllByParentIdAsync(category.Id, category.CompanyId);
        if (categories.Any())
            throw new ApiBadRequestException(CategoryErrors.CantBeDeletedWithChildren);
        category.Delete();
        await _categoryRepository.UpdateAsync(category);
        return _mapper.Map<CategoryDto>(category);
    }
}