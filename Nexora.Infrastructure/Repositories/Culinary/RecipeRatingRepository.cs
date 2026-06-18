using Microsoft.EntityFrameworkCore;

using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Culinary;

public class RecipeRatingRepository(CulinaryDbContext context) : IRecipeRatingRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<RecipeRating> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status, Guid companyId)
    {
        var query = _context.RecipeRatings
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId);

        if (!string.IsNullOrEmpty(name))
        {
            var pattern = $"%{name}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name ?? ""), EF.Functions.Unaccent(pattern)));
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

    public async Task<RecipeRating?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.RecipeRatings.FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task AddAsync(RecipeRating entity)
    {
        await _context.RecipeRatings.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RecipeRating entity)
    {
        await _context.SaveChangesAsync();
    }
}

