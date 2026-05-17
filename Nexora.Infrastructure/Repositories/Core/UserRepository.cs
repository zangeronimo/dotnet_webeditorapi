using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Core;

public class UserRepository(PlatformDbContext context) : IUserRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<(IEnumerable<User> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, string? email, Status? status)
    {
        var query = _context.Users
            .AsNoTracking();

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name), EF.Functions.Unaccent(pattern)));
        }

        if (!string.IsNullOrEmpty(email))
        {
            var pattern = $"%{email}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Email.Value), EF.Functions.Unaccent(pattern)));
        }

        if (status != null)
        {
            query = query.Where(c => c.Status == status);
        }

        // total before pagination
        var total = await query.CountAsync();

        query = OrderByHelper.ApplyOrdering(
            query,
            orderBy,
            desc,
            customMap: new Dictionary<string, Expression<Func<User, object?>>>
            {
                ["Email"] = x => x.Email.Value,
            },
            allowedFields:
            [
                "Name",
                "Email",
                "Status"
            ]
        );

        // pagination
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email);
    }
}
