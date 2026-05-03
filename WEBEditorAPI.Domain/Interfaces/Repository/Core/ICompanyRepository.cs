using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface ICompanyRepository
{
    Task<(IEnumerable<Company> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status);
    Task<Company?> GetByIdAsync(Guid id);
    Task<Company?> GetByNameAsync(string name);
    Task AddAsync(Company entity);
    Task UpdateAsync(Company entity);
}