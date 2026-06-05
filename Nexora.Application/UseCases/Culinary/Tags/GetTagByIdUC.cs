using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;

namespace Nexora.Application.UseCases.Culinary.Tags;

public class GetTagByIdUC(ITagRepository tagRepository, IMapper mapper) : IUseCase<GetByIdRequest, TagDto>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<TagDto> ExecuteAsync(GetByIdRequest request)
    {
        Tag? tag = await _tagRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (tag == null)
            throw new ApiNotFoundException(TagErrors.NotFound);
        return _mapper.Map<TagDto>(tag);
    }
}
