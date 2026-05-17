using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Interfaces.Repository.Core;
using Nexora.Infrastructure.Persistence;

namespace Nexora.Infrastructure.Repositories.Core;

public class PermissionRepository(PlatformDbContext context) : IPermissionRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<IReadOnlyList<string>> GetByUserCompanyAsync(Guid userCompanyId)
    {
        return await _context.UserCompanyModuleRoles
            .AsNoTracking()
            .Where(ucr =>
                ucr.UserCompanyId == userCompanyId &&
                ucr.UserCompany.Status == Status.Active &&
                ucr.Role.Status == Status.Active &&
                ucr.Module.Status == Status.Active)
            .Where(ucr =>
                ucr.UserCompany.Company.CompanyModules
                    .Any(cm => cm.ModuleId == ucr.ModuleId))
            .SelectMany(ucr => ucr.Role.RolePermissions
                .Where(rp =>
                    rp.Permission.Status == Status.Active &&
                    rp.Permission.ModuleId == ucr.ModuleId)
                .Select(rp => rp.Permission.Code))
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Permission>> GetByRangeIdAsync(List<Guid> rangeIds, Guid companyId)
    {
        return await _context.Permissions
            .Where(p => rangeIds.Contains(p.Id)
                     && p.Module.CompanyModules
                         .Any(cm => cm.CompanyId == companyId))
            .ToListAsync();
    }
}
