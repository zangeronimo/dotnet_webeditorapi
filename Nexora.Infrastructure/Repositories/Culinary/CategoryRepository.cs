using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Culinary;

public class CategoryRepository(CulinaryDbContext context) : ICategoryRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Category> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status, Guid companyId)
    {
        var query = _context.Categories
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId);

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name.Value), EF.Functions.Unaccent(pattern)));
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
            customMap: new Dictionary<string, Expression<Func<Category, object?>>>
            {
                ["Slug"] = x => x.Slug,
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

    private async Task<Category?> GetByIdInternalAsync(Guid id, Guid companyId, bool asNoTracking = false)
    {
        IQueryable<Category> query = _context.Categories;
        if (asNoTracking)
            query = query.AsNoTracking();
        return await query
            .FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task<Category?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await GetByIdInternalAsync(id, companyId, false);
    }

    public async Task<Category?> GetByIdReadOnlyAsync(Guid id, Guid companyId)
    {
        return await GetByIdInternalAsync(id, companyId, true);
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        await _context.SaveChangesAsync();
    }
    public async Task<Category?> GetBySlugAsync(Slug slug, Guid companyId)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Slug == slug && c.CompanyId == companyId);
    }
}

