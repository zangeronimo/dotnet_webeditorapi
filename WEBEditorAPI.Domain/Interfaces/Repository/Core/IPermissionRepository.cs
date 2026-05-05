using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IPermissionRepository
{
    Task<IReadOnlyList<string>> GetByUserCompanyAsync(Guid userCompanyId);
    Task<List<Permission>> GetByRangeIdAsync(List<Guid> rangeIds, Guid companyId);
}
