using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.Core;

public class ModuleRepository(PlatformDbContext context) : IModuleRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules.ToListAsync();
    }
    private async Task<Module?> GetByIdInternalAsync(Guid id, bool asNoTracking = false)
    {
        IQueryable<Module> query = _context.Modules;
        if (asNoTracking)
            query = query.AsNoTracking();
        return await query
            .Include(c => c.Permissions)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Module?> GetByIdAsync(Guid id)
    {
        return await GetByIdInternalAsync(id, false);
    }

    public async Task<Module?> GetByIdReadOnlyAsync(Guid id)
    {
        return await GetByIdInternalAsync(id, true);
    }

    public async Task<Module?> GetByNameAsync(string name)
    {
        return await _context.Modules.FirstOrDefaultAsync(m => EF.Functions.ILike(m.Name, name));
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
}