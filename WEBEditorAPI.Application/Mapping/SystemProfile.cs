using AutoMapper;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Application.Mapping;

public class SystemProfile : Profile
{
    public SystemProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Role, RoleDto>();
    }
}
