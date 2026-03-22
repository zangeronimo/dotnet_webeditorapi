using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<User>> GetAllAsync(Guid companyId)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Roles)
            .Where(user => user.CompanyId == companyId)
            .ToListAsync();
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

    public async Task DeleteAsync(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email);
    }
}
