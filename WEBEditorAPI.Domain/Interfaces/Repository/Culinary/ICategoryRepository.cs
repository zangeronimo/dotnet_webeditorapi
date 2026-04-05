using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

public interface ICategoryRepository : IRepository<Category>
{
    Task<(IEnumerable<Category> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? active,
        Guid companyId);
    Task<Category?> GetBySlugAsync(string slug);
}
