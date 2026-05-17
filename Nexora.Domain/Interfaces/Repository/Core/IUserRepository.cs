using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Interfaces.Repository.Core;

public interface IUserRepository
{
    Task<(IEnumerable<User> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        string? email,
        Status? status);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User entity);
    Task UpdateAsync(User entity);
}