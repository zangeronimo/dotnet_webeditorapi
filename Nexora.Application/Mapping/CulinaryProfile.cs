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
            .ForMember(dest => dest.MetaDescription, opt => opt.MapFrom(src => src.Seo.MetaDescription));

        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value));

        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value))
            .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Content.ShortDescription))
            .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.Content.FullDescription))
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Content.Sections))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Content.Notes))
            .ForMember(dest => dest.PrepTime, opt => opt.MapFrom(src => src.Timing.PrepTime))
            .ForMember(dest => dest.CookTime, opt => opt.MapFrom(src => src.Timing.CookTime))
            .ForMember(dest => dest.RestTime, opt => opt.MapFrom(src => src.Timing.RestTime))
            .ForMember(dest => dest.YieldTotal, opt => opt.MapFrom(src => src.Yield.YieldTotal))
            .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Attributes.Difficulty))
            .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => src.Attributes.Cuisine))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Media.ImageUrl))
            .ForMember(dest => dest.MetaTitle, opt => opt.MapFrom(src => src.Seo.MetaTitle))
            .ForMember(dest => dest.MetaDescription, opt => opt.MapFrom(src => src.Seo.MetaDescription));

        CreateMap<RecipeRating, RecipeRatingDto>()
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score.Value));
    }
}
