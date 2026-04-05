using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.System;

public class CategoryRepository(CulinaryDbContext context) : ICategoryRepository
{
    private readonly CulinaryDbContext _context = context;

    public async Task<(IEnumerable<Category> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? name, Status? active, Guid companyId)
    {
        var query = _context.Categories
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

        try
        {
            // dynamic ordenation
            if (!string.IsNullOrEmpty(orderBy))
            {
                query = desc
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }
        }
        catch
        {
            throw new ApiBadRequestException("OrderBy inválido.");
        }

        // pagination
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    public async Task<Category?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> GetBySlugAsync(string slug, Guid companyId)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Slug.Value == slug && c.CompanyId == companyId);
    }
}
