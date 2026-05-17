using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Infrastructure.Persistence;
using Nexora.Infrastructure.Persistence.Query;

namespace Nexora.Infrastructure.Repositories.Core;

public class UserCompanyRepository(PlatformDbContext context) : IUserCompanyRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<(IEnumerable<UserCompany> Items, int Total)> GetAllAsync(int page, int pageSize, string? orderBy, bool desc, string? nickName, Status? status, Guid companyId)
    {
        var query = _context.UserCompanies
            .AsNoTracking()
            .Where(uc => uc.CompanyId == companyId);

        if (!string.IsNullOrEmpty(nickName))
        {
            var pattern = $"%{nickName}%";
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.NickName ?? ""), EF.Functions.Unaccent(pattern)));
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
                "NickName",
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

    public async Task<List<UserCompanyModuleRole>> GetUserCompanyModuleRoleAsync(Guid userCompanyId)
    {
        return await _context.UserCompanyModuleRoles.AsNoTracking().Where(m => m.UserCompanyId == userCompanyId).ToListAsync();
    }

    public async Task<UserCompany?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.UserCompanies
            .Include(uc => uc.ModuleRoles)
            .Where(uc => uc.Id == id && uc.CompanyId == companyId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(UserCompany entity)
    {
        await _context.UserCompanies.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserCompany entity)
    {
        _context.UserCompanies.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<UserCompany>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserCompanies
            .Include(uc => uc.Company)
            .Include(uc => uc.User)
            .Where(uc => uc.UserId == userId).ToListAsync();
    }
}
