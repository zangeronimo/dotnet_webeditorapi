using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Application.Mapping;

public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<Module, ModuleDto>();
        CreateMap<Permission, PermissionDto>();
        CreateMap<Company, CompanyDto>();
    }
}
