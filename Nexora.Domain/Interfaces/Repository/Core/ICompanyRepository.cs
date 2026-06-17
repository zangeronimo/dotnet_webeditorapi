using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Interfaces.Repository.Core;

public interface ICompanyRepository
{
    Task<(IEnumerable<Company> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status);
    Task<Company?> GetByIdWithPermissionsAsync(Guid id);
    Task<Company?> GetByIdAsync(Guid id);
    Task<Company?> GetByIdReadOnlyAsync(Guid id);
    Task<Company?> GetByNameAsync(string name);
    Task AddAsync(Company entity);
    Task UpdateAsync(Company entity);
    Task<Company?> GetByIdWithApiClientsAsync(Guid companyId);
    Task<Company?> GetByIdWithApiClientsReadOnlyAsync(Guid companyId);
}