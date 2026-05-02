using Microsoft.EntityFrameworkCore;
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
}
