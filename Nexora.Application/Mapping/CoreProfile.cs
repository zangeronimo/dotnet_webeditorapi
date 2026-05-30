using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.DTOs.System;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Entities.System;

namespace Nexora.Application.Mapping;

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
