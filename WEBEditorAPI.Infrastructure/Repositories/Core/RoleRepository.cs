using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Persistence.Query;

namespace WEBEditorAPI.Infrastructure.Repositories.Core;

public class RoleRepository(PlatformDbContext context) : IRoleRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<(IEnumerable<Role> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status, Guid companyId)
    {
        var query = _context.Roles
            .AsNoTracking()
            .Where(r => r.CompanyId == companyId);

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

    private async Task<Role?> GetByIdInternalAsync(Guid id, Guid companyId, bool asNoTracking = false)
    {
        IQueryable<Role> query = _context.Roles;
        if (asNoTracking)
            query = query.AsNoTracking();
        return await query
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id && r.CompanyId == companyId);
    }

    public async Task<Role?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await GetByIdInternalAsync(id, companyId, false);
    }

    public async Task<Role?> GetByIdReadOnlyAsync(Guid id, Guid companyId)
    {
        return await GetByIdInternalAsync(id, companyId, true);
    }

    public async Task<Role?> GetByNameAsync(string name, Guid companyId)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(c => c.Name == name && c.CompanyId == companyId);
    }

    public async Task AddAsync(Role entity)
    {
        await _context.Roles.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role entity)
    {
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();
    }
}
