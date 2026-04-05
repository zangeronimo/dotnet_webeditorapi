using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.Interfaces.Repository.Culinary;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.Culinary;

public class LevelRepository : ILevelRepository
{
    private readonly CulinaryDbContext _context;

    public LevelRepository(CulinaryDbContext context)
    {
        _context = context;
    }

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

    public async Task<Level?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.Levels
            .FirstOrDefaultAsync(c => c.Id == id && c.CompanyId == companyId);
    }

    public async Task AddAsync(Level entity)
    {
        await _context.Levels.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Level entity)
    {
        _context.Levels.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Level?> GetBySlugAsync(string slug, Guid companyId)
    {
        return await _context.Levels.FirstOrDefaultAsync(c => c.Slug.Value == slug && c.CompanyId == companyId);
    }
}

