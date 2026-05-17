using Microsoft.EntityFrameworkCore;
using Nexora.Infrastructure.Persistence;

namespace Nexora.Tests.Infrastructure;

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