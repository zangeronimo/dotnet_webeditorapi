using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.System;

public class ModuleRepository : IModuleRepository
{
    private readonly AppDbContext _context;

    public ModuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules.ToListAsync();
    }

    public async Task<Module?> GetByIdAsync(Guid id)
    {
        return await _context.Modules.FindAsync(id);
    }

    public async Task AddAsync(Module entity)
    {
        await _context.Modules.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Module entity)
    {
        _context.Modules.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Module entity)
    {
        _context.Modules.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public Task<List<Role>> GetAllRolesByIdsAsync(List<Guid> roleIds, Guid companyId)
    {
        return _context.Roles
            .Where(r => roleIds.Contains(r.Id))
            .Where(r =>
                _context.Companies
                    .Where(c => c.Id == companyId)
                    .SelectMany(c => c.Modules)
                    .Any(m => m.Id == r.ModuleId)
            )
            .ToListAsync();
    }
}