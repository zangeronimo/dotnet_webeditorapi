using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Persistence.Query;

namespace WEBEditorAPI.Infrastructure.Repositories.Culinary;

public class LevelRepository(CulinaryDbContext context) : ILevelRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Level> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? active, Guid companyId)
    {
        var query = _context.Levels
            .AsNoTracking()
            .Include(c => c.Categories)
            .Where(c => c.CompanyId == companyId);

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name), EF.Functions.Unaccent(pattern)));
        }

        if (active != null)
        {
            query = query.Where(c => c.Active == active);
        }

        // total before pagination
        var total = await query.CountAsync();

        query = OrderByHelper.ApplyOrdering(
            query,
            orderBy,
            desc,
            customMap: new Dictionary<string, Expression<Func<Level, object?>>>
            {
                ["Slug"] = x => x.Slug.Value,
            },
            allowedFields:
            [
                "Name",
                "Slug",
                "Active"
            ]
        );

        // pagination
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    private async Task<Level?> GetByIdInternalAsync(Guid id, Guid companyId, bool asNoTracking = false)
    {
        IQueryable<Level> query = _context.Levels;
        if (asNoTracking)
            query = query.AsNoTracking();
        return await query
            .Include(c => c.Categories)
            .FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task<Level?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await GetByIdInternalAsync(id, companyId, false);
    }

    public async Task<Level?> GetByIdReadOnlyAsync(Guid id, Guid companyId)
    {
        return await GetByIdInternalAsync(id, companyId, true);
    }

    public async Task AddAsync(Level entity)
    {
        await _context.Levels.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Level entity)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Level?> GetBySlugAsync(string slug, Guid companyId)
    {
        return await _context.Levels
            .Include(c => c.Categories)
            .FirstOrDefaultAsync(c => c.Slug.Value == slug && c.CompanyId == companyId);
    }

    public async Task<Category?> GetCategoryBySlugAsync(string slug, Guid companyId)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Slug.Value == slug && c.CompanyId == companyId);
    }
}

