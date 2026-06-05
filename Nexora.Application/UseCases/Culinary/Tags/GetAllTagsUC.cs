using AutoMapper;

using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Tags;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Tags;

public class GetAllTagsUC(ITagRepository tagRepository, IMapper mapper) : IUseCase<GetAllTagsFilterRequest, PaginationResult<TagDto>>
{
    private readonly ITagRepository _tagRepository = tagRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<TagDto>> ExecuteAsync(GetAllTagsFilterRequest request)
    {
        (IEnumerable<Tag> tags, int total) = await _tagRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Name, request.Status, request.Context.CompanyId);

        return new PaginationResult<TagDto>
        {
            Items = _mapper.Map<IEnumerable<TagDto>>(tags),
            Total = total
        };
    }
}
