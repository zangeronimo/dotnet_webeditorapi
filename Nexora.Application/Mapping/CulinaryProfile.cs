using AutoMapper;

using Nexora.Application.DTOs.Culinary;
using Nexora.Domain.Entities.Culinary;

namespace Nexora.Application.Mapping;

public class CulinaryProfile : Profile
{
    public CulinaryProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value))
            .ForMember(dest => dest.MetaTitle, opt => opt.MapFrom(src => src.Seo.MetaTitle))
            .ForMember(dest => dest.MetaDescription, opt => opt.MapFrom(src => src.Seo.MetaDescription))
            .ForMember(dest => dest.CanonicalUrl, opt => opt.MapFrom(src => src.Seo.CanonicalUrl));

        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value));
    }
}
