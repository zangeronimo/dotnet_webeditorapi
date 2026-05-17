using System;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Interfaces.Repository.Culinary;

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
}
