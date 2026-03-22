using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Domain.Interfaces.Repository.System;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(Guid id);
    Task AddAsync(Company entity);
    Task UpdateAsync(Company entity);
    Task DeleteAsync(Company entity);
}