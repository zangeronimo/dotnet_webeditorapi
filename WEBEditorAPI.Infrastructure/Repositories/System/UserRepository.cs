using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.System;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<User> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, string? email, Guid companyId)
    {
        var query = _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .Where(u => u.CompanyId == companyId);

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(u => EF.Functions.ILike(EF.Functions.Unaccent(u.Name), EF.Functions.Unaccent(pattern)));
        }

        if (!string.IsNullOrEmpty(email))
        {
            var pattern = $"%{email}%";
            query = query.Where(u => EF.Functions.ILike(EF.Functions.Unaccent(u.Email.Value), EF.Functions.Unaccent(pattern)));
        }

        // total before pagination
        var total = await query.CountAsync();

        try
        {
            // dynamic ordenation
            if (!string.IsNullOrEmpty(orderBy))
            {
                query = desc
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }
        }
        catch
        {
            throw new ApiBadRequestException("OrderBy inválido.");
        }

        // pagination
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    public async Task<User?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == id && user.CompanyId == companyId);
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
