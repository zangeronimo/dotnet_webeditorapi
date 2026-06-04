
using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Categories;

public class GetCategoryByIdUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<GetByIdRequest, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CategoryDto> ExecuteAsync(GetByIdRequest request)
    {
        Category? category = await _categoryRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (category == null)
            throw new ApiNotFoundException(CategoryErrors.NotFound);
        return _mapper.Map<CategoryDto>(category);
    }
}
