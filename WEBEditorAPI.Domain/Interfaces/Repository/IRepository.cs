
namespace WEBEditorAPI.Domain.Interfaces.Repository;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, Guid companyId);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}
