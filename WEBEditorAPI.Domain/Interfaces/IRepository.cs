
namespace WEBEditorAPI.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetOneAsync(Func<T, bool> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
