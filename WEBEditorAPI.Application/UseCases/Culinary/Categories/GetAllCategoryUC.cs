using System;
using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Culinary.Categories;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

namespace WEBEditorAPI.Application.UseCases.Culinary.Categories;

public class GetAllCategoryUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<CategoryDto>> ExecuteAsync(GetAllCategoriesFilterRequest request)
    {
        (IEnumerable<Category> categories, int total) = await _categoryRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Active, request.Context.CompanyId);

        return new PaginationResult<CategoryDto>
        {
            Items = _mapper.Map<IEnumerable<CategoryDto>>(categories),
            Total = total
        };
    }
}