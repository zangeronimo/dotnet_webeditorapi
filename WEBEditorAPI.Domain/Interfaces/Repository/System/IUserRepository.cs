using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Domain.Interfaces.Repository.System;

public interface IUserRepository : IRepository<User>
{
    Task<(IEnumerable<User> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        string? email,
        Guid companyId);
    Task<User?> GetByEmailAsync(string email);
}