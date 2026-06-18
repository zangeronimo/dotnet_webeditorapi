using Nexora.Domain.Entities.Core;

namespace Nexora.Domain.Interfaces.Repository.System;

public interface IApiClientRepository : IRepository<ApiClient>
{
    Task<IEnumerable<ApiClient>> GetAllAsync(Guid companyId);
}
