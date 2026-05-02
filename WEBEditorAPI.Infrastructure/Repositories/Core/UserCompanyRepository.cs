using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Infrastructure.Repositories.Core;

public class UserCompanyRepository(PlatformDbContext context) : IUserCompanyRepository
{
    private readonly PlatformDbContext _context = context;

    public async Task<UserCompany?> GetByIdAsync(Guid id, Guid companyId)
    {
        return await _context.UserCompanies
            .Where(uc => uc.Id == id && uc.CompanyId == companyId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(UserCompany entity)
    {
        await _context.UserCompanies.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserCompany entity)
    {
        _context.UserCompanies.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<UserCompany>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserCompanies.Where(uc => uc.UserId == userId).ToListAsync();
    }
}
