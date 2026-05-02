using System;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Domain.Interfaces.Repository.Core;

public interface IUserCompanyRepository : IRepository<UserCompany>
{
    Task<IReadOnlyList<UserCompany>> GetByUserIdAsync(Guid userId);
}
