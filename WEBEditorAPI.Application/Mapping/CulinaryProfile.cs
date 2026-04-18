using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Application.Mapping;

public class CulinaryProfile : Profile
{
    public CulinaryProfile()
    {
        CreateMap<Level, LevelDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value));

        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug.Value))
            .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Content.ShortDescription))
            .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.Content.FullDescription))
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Content.Ingredients))
            .ForMember(dest => dest.Preparation, opt => opt.MapFrom(src => src.Content.Preparation))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Content.Notes))
            .ForMember(dest => dest.PrepTime, opt => opt.MapFrom(src => src.Timing.PrepTime))
            .ForMember(dest => dest.CookTime, opt => opt.MapFrom(src => src.Timing.CookTime))
            .ForMember(dest => dest.RestTime, opt => opt.MapFrom(src => src.Timing.RestTime))
            .ForMember(dest => dest.YieldTotal, opt => opt.MapFrom(src => src.Yield.YieldTotal))
            .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Attributes.Difficulty))
            .ForMember(dest => dest.Tools, opt => opt.MapFrom(src => src.Attributes.Tools))
            .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => src.Attributes.Cuisine))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Media.ImageUrl))
            .ForMember(dest => dest.MetaTitle, opt => opt.MapFrom(src => src.Seo.MetaTitle))
            .ForMember(dest => dest.MetaDescription, opt => opt.MapFrom(src => src.Seo.MetaDescription))
            .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => string.Join(", ", src.Seo.Keywords)))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.Engagement.Views))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Engagement.Likes));
    }
}
