using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Persistence.Query;

namespace WEBEditorAPI.Infrastructure.Repositories.Culinary;

public class RecipeRepository(CulinaryDbContext context) : IRecipeRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Recipe> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? active, Guid companyId)
    {
        var query = _context.Recipes
            .AsNoTracking()
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
            customMap: new Dictionary<string, Expression<Func<Recipe, object?>>>
            {
                ["Slug"] = x => x.Slug.Value,
                ["Image"] = x => x.Media.ImageUrl,
                ["Views"] = x => x.Engagement.Views
            },
            allowedFields:
            [
                "Name",
                "Slug",
                "Image",
                "Views",
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

    public async Task<Recipe?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Recipes.FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
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

    public async Task<Recipe?> GetBySlugAsync(string slug, Guid companyId)
    {
        return await _context.Recipes
            .FirstOrDefaultAsync(c => c.Slug.Value == slug && c.CompanyId == companyId);
    }
}
