using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Culinary;

public class TagRepository(CulinaryDbContext context) : ITagRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Tag> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status, Guid companyId)
    {
        var query = _context.Tags
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId);

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
            customMap: new Dictionary<string, Expression<Func<Tag, object?>>>
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

    public async Task<Tag?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Tags.FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task AddAsync(Tag entity)
    {
        await _context.Tags.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag entity)
    {
        await _context.SaveChangesAsync();
    }
    public async Task<Tag?> GetBySlugAsync(Slug slug, Guid companyId)
    {
        return await _context.Tags
            .FirstOrDefaultAsync(c => c.Slug == slug && c.CompanyId == companyId);
    }
}

