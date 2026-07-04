using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Culinary;
using Nexora.Domain.ValueObjects;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Culinary;

public class RecipeRepository(CulinaryDbContext context) : IRecipeRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Recipe> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? status, Guid? categoryId, Guid companyId)
    {
        var query = _context.Recipes
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

        if (categoryId != null)
        {
            query = query.Where(c => c.CategoryId == categoryId);
        }

        // total before pagination
        var total = await query.CountAsync();

        query = OrderByHelper.ApplyOrdering(
            query,
            orderBy,
            desc,
            customMap: new Dictionary<string, Expression<Func<Recipe, object?>>>
            {
                ["Slug"] = x => x.Slug,
                ["HasImage"] = x => x.Media.ImageUrl == null || x.Media.ImageUrl == "",
            },
            allowedFields:
            [
                "Name",
                "Slug",
                "HasImage",
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

    public async Task<Recipe?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Recipes.Include(r => r.Category).ThenInclude(c => c.Parent).FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task AddAsync(Recipe entity)
    {
        await _context.Recipes.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Recipe entity)
    {
        await _context.SaveChangesAsync();
    }
    public async Task<Recipe?> GetBySlugAsync(Slug slug, Guid companyId)
    {
        return await _context.Recipes
            .FirstOrDefaultAsync(c => c.Slug == slug && c.CompanyId == companyId);
    }
}