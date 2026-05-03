using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Persistence.Query;

namespace WEBEditorAPI.Infrastructure.Repositories.Core;

public class CompanyRepository(PlatformDbContext context) : ICompanyRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<(IEnumerable<Company> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status)
    {
        var query = _context.Companies
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

    private async Task<Company?> GetByIdInternalAsync(Guid id, bool asNoTracking = false)
    {
        IQueryable<Company> query = _context.Companies;
        if (asNoTracking)
            query = query.AsNoTracking();
        return await query
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await GetByIdInternalAsync(id, false);
    }

    public async Task<Company?> GetByIdReadOnlyAsync(Guid id)
    {
        return await GetByIdInternalAsync(id, true);
    }

    public async Task<Company?> GetByNameAsync(string name)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task AddAsync(Company entity)
    {
        await _context.Companies.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company entity)
    {
        _context.Companies.Update(entity);
        await _context.SaveChangesAsync();
    }
}
