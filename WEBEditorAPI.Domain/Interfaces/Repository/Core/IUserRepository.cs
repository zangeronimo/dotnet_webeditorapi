
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User entity);
    Task UpdateAsync(User entity);
}