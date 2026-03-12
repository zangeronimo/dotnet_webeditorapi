using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Domain.Interfaces;
using WEBEditorAPI.Domain.Interfaces.System;
using WEBEditorAPI.Infrastructure.Persistence;
using WEBEditorAPI.Infrastructure.Repositories.System;

namespace WEBEditorAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        // Repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}
