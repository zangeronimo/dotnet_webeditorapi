using System;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IRoleRepository : IRepository<Role>
{
    Task<(IEnumerable<Role> Items, int Total)> GetAllAsync(
            int page,
            int pageSize,
            string? orderBy,
            bool desc,
            string? name,
            Status? status,
            Guid companyId);
    Task<List<Role>> GetByRangeIdAsync(List<Guid> rangeIds);
    Task<List<Role>> GetAllByCompanyIdAsync(Guid companyId);
    Task<Role?> GetByNameAsync(string name, Guid companyId);
    Task<Role?> GetByIdReadOnlyAsync(Guid id, Guid companyId);
}
