using AutoMapper;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.System.Users;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Application.UseCases.System.Users;

public class UpdateUserUC(IUserRepository userRepository, IModuleRepository moduleRepository, IPasswordProvider passwordProvider, IMapper mapper) : IUseCase<UpdateUserRequest, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDto> ExecuteAsync(UpdateUserRequest request)
    {
        User? emailExists = await _userRepository.GetByEmailAsync(request.Email);
        if (emailExists != null && emailExists.Id != request.Id)
            throw new ApiBadRequestException("Usuário já cadastrado com esse e-mail");
        User? user = await _userRepository.GetByIdAsync((Guid)request.Id, request.Context.CompanyId);
        if (user == null || request.Id == Guid.Empty)
            throw new ApiNotFoundException("Usuário não encontrado");
        user!.Update(request.Name, Email.Create(request.Email));
        if (!string.IsNullOrEmpty(request.Password))
            user.UpdatePassword(Password.Create(request.Password, _passwordProvider));
        var roles = await GetRolesByIdsAsync(request.RoleIds, user.CompanyId);
        user.UpdateRoles(roles);
        await _userRepository.UpdateAsync(user);
        User? updatedUser = await _userRepository.GetByIdAsync(user.Id, user.CompanyId);
        return _mapper.Map<UserDto>(updatedUser);
    }

    private async Task<List<Role>> GetRolesByIdsAsync(List<Guid> roleIds, Guid companyId)
    {
        var distinctIds = roleIds.Distinct().ToList();
        var roles = await _moduleRepository.GetAllRolesByIdsAsync(distinctIds, companyId);
        if (roles.Count != distinctIds.Count)
            throw new ApiBadRequestException("Um ou mais roleIds informados são inválidos.");
        return roles;
    }
}
