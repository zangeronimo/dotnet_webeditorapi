

using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IModuleRepository
{
    Task<IEnumerable<Module>> GetAllAsync();
    Task<Module?> GetByIdAsync(Guid id);
    Task<Module?> GetByNameAsync(string name);
    Task AddAsync(Module entity);
    Task UpdateAsync(Module entity);
    Task DeleteAsync(Module entity);
    Task<Module?> GetByIdReadOnlyAsync(Guid id);
}