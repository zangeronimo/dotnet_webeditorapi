
using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Categories;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Categories;

public class GetByParentIdUC(ICategoryRepository categoryRepository, IMapper mapper) : IUseCase<GetByParentIdRequest, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CategoryDto>> ExecuteAsync(GetByParentIdRequest request)
    {
        IEnumerable<Category> categories = await _categoryRepository.GetByParentIdAsync(request.ParentId, request.Context.CompanyId);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
}
