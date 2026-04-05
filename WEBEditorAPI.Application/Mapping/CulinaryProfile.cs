using AutoMapper;
using WEBEditorAPI.Application.DTOs.Culinary;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Application.Mapping;

public class CulinaryProfile : Profile
{
    public CulinaryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Level, LevelDto>();
    }
}
