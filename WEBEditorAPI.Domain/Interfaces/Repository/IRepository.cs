
namespace WEBEditorAPI.Domain.Interfaces.Repository;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(Guid companyId);
    Task<T?> GetByIdAsync(Guid id, Guid companyId);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
