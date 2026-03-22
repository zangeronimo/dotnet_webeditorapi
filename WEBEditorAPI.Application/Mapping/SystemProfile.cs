using AutoMapper;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Application.Mapping;

public class SystemProfile : Profile
{
    public SystemProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));

        CreateMap<Role, RoleDto>();
    }
}
