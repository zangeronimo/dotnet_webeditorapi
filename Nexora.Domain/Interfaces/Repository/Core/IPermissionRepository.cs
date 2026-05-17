using Nexora.Domain.Entities.Core;

namespace Nexora.Domain.Interfaces.Repository.Core;

public interface IPermissionRepository
{
    Task<IReadOnlyList<string>> GetByUserCompanyAsync(Guid userCompanyId);
    Task<List<Permission>> GetByRangeIdAsync(List<Guid> rangeIds, Guid companyId);
}
