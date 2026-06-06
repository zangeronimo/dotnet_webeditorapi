using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Interfaces.Repository.Culinary;

public interface IRecipeRatingRepository : IRepository<RecipeRating>
{
    Task<(IEnumerable<RecipeRating> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status,
        Guid companyId);
}
