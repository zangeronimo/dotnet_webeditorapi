using Microsoft.EntityFrameworkCore;

using Nexora.Application.Exceptions;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Interfaces.Repository.System;
using Nexora.Infrastructure.Persistence;

namespace Nexora.Infrastructure.Repositories.System;

public class ApiClientRepository(PlatformDbContext context) : IApiClientRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<IEnumerable<ApiClient>> GetAllAsync(Guid companyId)
    {
        return await _context.ApiClients.AsNoTracking().OrderBy(a => a.Name).Where(a => a.CompanyId == companyId).ToListAsync();
    }

    public async Task<ApiClient?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.ApiClients.FirstOrDefaultAsync(a => a.Id == id && a.CompanyId == companyId);
    }

    public async Task AddAsync(ApiClient entity)
    {
        throw new ApiBadRequestException("Operation not allowed.");
    }

    public async Task UpdateAsync(ApiClient entity)
    {
        _context.ApiClients.Update(entity);
        await _context.SaveChangesAsync();
    }
}
