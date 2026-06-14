using AutoMapper;

using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Categories;

public class GetAllCategoriesUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<GetAllCategoriesFilterRequest, PaginationResult<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<CategoryDto>> ExecuteAsync(GetAllCategoriesFilterRequest request)
    {
        (IEnumerable<Category> categories, int total) = await _categoryRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status, request.Parent, request.Context.CompanyId);

        return new PaginationResult<CategoryDto>
        {
            Items = _mapper.Map<IEnumerable<CategoryDto>>(categories),
            Total = total
        };
    }
}
