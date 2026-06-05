using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Nexora.Domain.Interfaces.Repository.Culinary;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<(IEnumerable<Recipe> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status,
        Guid? categoryId,
        Guid companyId);

    Task<Recipe?> GetBySlugAsync(Slug slug, Guid companyId);
}
