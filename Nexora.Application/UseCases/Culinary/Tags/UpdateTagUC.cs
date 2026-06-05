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

public class UpdateTagUC(ITagRepository tagRepository, IMapper mapper) : IUseCase<UpdateTagRequest, TagDto>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<TagDto> ExecuteAsync(UpdateTagRequest request)
    {
        var slug = Slug.Create(request.Name);
        Tag? tag = await _tagRepository.GetBySlugAsync(slug, request.Context.CompanyId);
        if (tag != null && tag.Id != request.Id)
            throw new ApiBadRequestException(TagErrors.AlreadyExists);
        Tag? updateTag = await _tagRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (updateTag == null)
            throw new ApiBadRequestException(TagErrors.NotFound);
        updateTag.Update(slug, request.Name, request.Description, request.Status);
        await _tagRepository.UpdateAsync(updateTag);
        Tag? updatedTag = await _tagRepository.GetByIdAsync(updateTag.Id, request.Context.CompanyId);
        return _mapper.Map<TagDto>(updatedTag);
    }
}