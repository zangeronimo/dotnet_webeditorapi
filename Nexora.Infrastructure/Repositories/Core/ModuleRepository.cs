using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Core;

public class ModuleRepository(PlatformDbContext context) : IModuleRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<(IEnumerable<Module> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status)
    {
        var query = _context.Modules
            .AsNoTracking();

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name), EF.Functions.Unaccent(pattern)));
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
            allowedFields:
            [
                "Name",
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

    public async Task<List<Module>> GetByRangeIdAsync(List<Guid> rangeIds)
    {
        return await _context.Modules.Include(m => m.CompanyModules).Where(m => rangeIds.Contains(m.Id)).ToListAsync();
    }

    public async Task<List<Module>> GetAllByCompanyIdAsync(Guid companyId)
    {
        return await _context.Modules.AsNoTracking().Where(m => m.CompanyModules.Any(cm => cm.CompanyId == companyId)).ToListAsync();
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