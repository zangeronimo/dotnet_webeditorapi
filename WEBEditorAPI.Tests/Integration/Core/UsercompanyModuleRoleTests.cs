using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Tests.Infrastructure;

namespace WEBEditorAPI.Tests.Integration.Core;

public class UserCompanyModuleRoleTests
{
    [Fact]
    public void Should_Save_And_Retrieve_UserCompanyModuleRole()
    {
        // Arrange
        var context = DbContextFactory.Create();

        var userCompanyId = Guid.NewGuid();
        var moduleId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var entity = new UserCompanyModuleRole(
            userCompanyId,
            moduleId,
            roleId
        );

        // Act
        context.Set<UserCompanyModuleRole>().Add(entity);
        context.SaveChanges();

        var result = context.Set<UserCompanyModuleRole>().FirstOrDefault();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userCompanyId, result!.UserCompanyId);
        Assert.Equal(moduleId, result.ModuleId);
        Assert.Equal(roleId, result.RoleId);
    }

    [Fact]
    public void Should_Filter_By_UserCompanyId()
    {
        var context = DbContextFactory.Create();

        var uc1 = Guid.NewGuid();
        var uc2 = Guid.NewGuid();

        context.Add(new UserCompanyModuleRole(uc1, Guid.NewGuid(), Guid.NewGuid()));
        context.Add(new UserCompanyModuleRole(uc2, Guid.NewGuid(), Guid.NewGuid()));

        context.SaveChanges();

        var result = context.Set<UserCompanyModuleRole>()
            .Where(x => x.UserCompanyId == uc1)
            .ToList();

        Assert.Single(result);
    }
}
