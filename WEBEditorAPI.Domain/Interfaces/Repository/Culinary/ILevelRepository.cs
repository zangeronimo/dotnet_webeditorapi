using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Culinary;

public interface ILevelRepository : IRepository<Level>
{
    Task<(IEnumerable<Level> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? active,
        Guid companyId);
    Task<Level?> GetBySlugAsync(string slug, Guid companyId);
}
