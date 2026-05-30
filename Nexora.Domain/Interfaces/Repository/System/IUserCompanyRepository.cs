using Nexora.Domain.Entities.System;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Interfaces.Repository.System;

public interface IUserCompanyRepository : IRepository<UserCompany>
{
    Task<(IEnumerable<UserCompany> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? nickName,
        Status? status,
        Guid companyId);
    Task<IReadOnlyList<UserCompany>> GetByUserIdAsync(Guid userId);
    Task<List<UserCompanyModuleRole>> GetUserCompanyModuleRoleAsync(Guid userCompanyId);
}
