using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Nexora.Domain.Interfaces.Repository.Culinary;

public interface ITagRepository : IRepository<Tag>
{
    Task<(IEnumerable<Tag> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? name,
        Status? status,
        Guid companyId);

    Task<Tag?> GetBySlugAsync(Slug slug, Guid companyId);
}
