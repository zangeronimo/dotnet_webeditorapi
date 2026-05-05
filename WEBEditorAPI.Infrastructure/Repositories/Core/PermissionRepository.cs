using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.Core;

public class PermissionRepository(PlatformDbContext context) : IPermissionRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<IReadOnlyList<string>> GetByUserCompanyAsync(Guid userCompanyId)
    {
        return await _context.UserCompanyModuleRoles
            .Where(ucr => ucr.UserCompanyId == userCompanyId)
            .SelectMany(ucr => ucr.Role.Permissions
                .Where(p => p.ModuleId == ucr.ModuleId)
                .Select(p => p.Code))
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Permission>> GetByRangeIdAsync(List<Guid> rangeIds, Guid companyId)
    {
        var sql = @"
            SELECT p.*
            FROM core_permissions p
            INNER JOIN core_company_modules cm ON cm.module_id = p.module_id
            WHERE p.deleted_at IS NULL
            AND cm.company_id = @companyId
            AND p.id = ANY(@rangeIds)
        ";

        return await _context.Permissions
            .FromSqlRaw(sql,
                new Npgsql.NpgsqlParameter("@companyId", companyId),
                new Npgsql.NpgsqlParameter("@rangeIds", rangeIds))
            .ToListAsync();
    }
}
