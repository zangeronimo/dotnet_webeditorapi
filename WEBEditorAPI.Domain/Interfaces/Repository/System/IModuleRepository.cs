
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Domain.Interfaces.Repository.System;

public interface IModuleRepository
{
    Task<IEnumerable<Module>> GetAllAsync();
    Task<Module?> GetByIdAsync(Guid id);
    Task AddAsync(Module entity);
    Task UpdateAsync(Module entity);
    Task DeleteAsync(Module entity);
}