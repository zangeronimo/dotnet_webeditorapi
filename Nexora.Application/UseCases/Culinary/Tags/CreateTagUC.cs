using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.Culinary.Tags;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Errors.Culinary;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects;

namespace Nexora.Application.UseCases.Culinary.Tags;

public class CreateTagUC(ITagRepository tagRepository, IMapper mapper) : IUseCase<CreateTagRequest, TagDto>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<TagDto> ExecuteAsync(CreateTagRequest request)
    {
        var slug = Slug.Create(request.Name);
        Tag? tag = await _tagRepository.GetBySlugAsync(slug, request.Context.CompanyId);
        if (tag != null)
            throw new ApiBadRequestException(TagErrors.AlreadyExists);
        Tag newTag = new Tag(slug, request.Name, request.Description, request.Status, request.Context.CompanyId);
        await _tagRepository.AddAsync(newTag);
        Tag? createdTag = await _tagRepository.GetByIdAsync(newTag.Id, request.Context.CompanyId);
        return _mapper.Map<TagDto>(createdTag);
    }
}
