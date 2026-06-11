using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Nexora.Domain.Interfaces.Repository.Culinary;

public interface ICategoryRepository : IRepository<Category>
{
    Task<(IEnumerable<Category> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status,
        Guid? parent,
        Guid companyId);
    Task<IEnumerable<Category>> GetByParentIdAsync(Guid parentId, Guid companyId);
    Task<Category?> GetBySlugAsync(Slug slug, Guid companyId);
    Task<IEnumerable<Category>> GetAllByParentIdAsync(Guid parentId, Guid companyId);
    Task<IEnumerable<Category>> GetAllParentsAsync(Guid companyId);
}
