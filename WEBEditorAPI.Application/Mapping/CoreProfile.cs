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
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Modules,
                opt => opt.MapFrom(src => src.CompanyModules
                    .Select(cm => cm.Module)));
        CreateMap<User, UserDto>();
        CreateMap<UserCompany, UserCompanyDto>();
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => src.RolePermissions
                    .Select(rp => rp.Permission)));
    }
}
