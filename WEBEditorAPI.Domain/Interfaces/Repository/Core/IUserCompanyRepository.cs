using System;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IUserCompanyRepository : IRepository<UserCompany>
{
    Task<(IEnumerable<UserCompany> Items, int Total)> GetAllAsync(
        int page,
        int pageSize,
        string? orderBy,
        bool desc,
        string? nickName,
        Status? status,
        Guid companyId);
    Task<IReadOnlyList<UserCompany>> GetByUserIdAsync(Guid userId);
}
