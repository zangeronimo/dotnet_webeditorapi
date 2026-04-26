using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Tests.Infrastructure;

namespace WEBEditorAPI.Tests.Integration.Core;

public class RoleTests
{
    [Fact]
    public void Should_Save_Role_With_Permissions()
    {
        // Arrange
        var context = DbContextFactory.Create();

        var companyId = Guid.NewGuid();
        var moduleId = Guid.NewGuid();

        var permission1 = new Permission("module.view", "View", Status.Active, moduleId);
        var permission2 = new Permission("module.update", "Update", Status.Active, moduleId);

        var role = new Role("Admin", Status.Active, companyId);

        role.Permissions.Add(permission1);
        role.Permissions.Add(permission2);

        // Act
        context.Add(role);
        context.SaveChanges();

        var saved = context.Set<Role>()
            .Where(r => r.Id == role.Id)
            .Select(r => new
            {
                r.Id,
                Permissions = r.Permissions
            })
            .First();

        // Assert
        Assert.Equal(2, saved.Permissions.Count);
    }

    [Fact]
    public void Should_Add_And_Remove_Permission_From_Role()
    {
        var context = DbContextFactory.Create();

        var role = new Role("Admin", Status.Active, Guid.NewGuid());
        var permission = new Permission("module.view", "View", Status.Active, Guid.NewGuid());

        role.Permissions.Add(permission);

        context.Add(role);
        context.SaveChanges();

        // remove
        role.Permissions.Remove(permission);
        context.SaveChanges();

        var saved = context.Set<Role>()
            .Select(r => r.Permissions)
            .First();

        Assert.Empty(saved);
    }
}
