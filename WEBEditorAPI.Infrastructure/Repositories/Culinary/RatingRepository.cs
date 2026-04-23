using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Persistence.Query;

namespace WEBEditorAPI.Infrastructure.Repositories.Culinary;

public class RatingRepository(CulinaryDbContext context) : IRatingRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Rating> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? active, Guid companyId)
    {
        var query = _context.Ratings
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId);

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name ?? ""), EF.Functions.Unaccent(pattern)));
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
            customMap: new Dictionary<string, Expression<Func<Rating, object?>>>
            { },
            allowedFields:
            [
                "Name",
                "Rate",
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

    public async Task<Rating?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Ratings.FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task AddAsync(Rating entity)
    {
        await _context.Ratings.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rating entity)
    {
        await _context.SaveChangesAsync();
    }
}
