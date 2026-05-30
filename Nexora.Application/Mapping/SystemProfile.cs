using AutoMapper;
using Nexora.Application.DTOs.System;
using Nexora.Domain.Entities.System;

namespace Nexora.Application.Mapping;

public class SystemProfile : Profile
{
    public SystemProfile()
    {
        CreateMap<UserCompany, UserCompanyDto>();
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => src.RolePermissions
                    .Select(rp => rp.Permission)));
    }
}
