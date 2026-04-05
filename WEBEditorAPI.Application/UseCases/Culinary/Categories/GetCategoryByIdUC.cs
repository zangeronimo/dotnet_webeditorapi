using System;
using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Categories;

public class GetCategoryByIdUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<GetByIdRequest, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<CategoryDto> ExecuteAsync(GetByIdRequest request)
    {
        Category? category = await _categoryRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (category == null)
            throw new ApiNotFoundException("Categoria não encontrado");
        return _mapper.Map<CategoryDto>(category);
    }
}
