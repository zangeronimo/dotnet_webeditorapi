

using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IModuleRepository
{
    Task<(IEnumerable<Module> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status);
    Task<List<Module>> GetByRangeIdAsync(List<Guid> rangeIds);
    Task<Module?> GetByIdAsync(Guid id);
    Task<Module?> GetByNameAsync(string name);
    Task AddAsync(Module entity);
    Task UpdateAsync(Module entity);
    Task DeleteAsync(Module entity);
    Task<Module?> GetByIdReadOnlyAsync(Guid id);
}