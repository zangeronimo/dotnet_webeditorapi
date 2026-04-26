using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Infrastructure.Persistence;

namespace WEBEditorAPI.Tests.Infrastructure;

public static class DbContextFactory
{
    public static PlatformDbContext Create()
    {
        var options = new DbContextOptionsBuilder<PlatformDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new PlatformDbContext(options);

        return context;
    }
}