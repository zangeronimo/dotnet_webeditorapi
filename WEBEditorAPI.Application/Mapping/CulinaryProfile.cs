using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Application.Mapping;

public class CulinaryProfile : Profile
{
    public CulinaryProfile()
    {
        CreateMap<Level, LevelDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        CreateMap<Category, CategoryDto>();
    }
}
