using Microsoft.AspNetCore.Identity;
using Moq;
using WEBEditorAPI.Application.UseCases.System;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.System;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Tests.Application.UseCases;

public class MakeLoginUCTests
{
    [Fact]
    public async Task Should_Login_And_Return_Tokens()
    {
        // Arrange
        var companyId = Guid.NewGuid();

        var permissions = new List<string>
    {
        "product.read",
        "product.write"
    };

        var user = new User("Luciano Zangeronimo", Email.Create("zangeronimo@gmail.com"), Password.Restore("123"), WEBEditorAPI.Domain.Enums.Status.Active);
        var userId = user.Id;

        var userRepository = new Mock<IUserRepository>();
        userRepository
            .Setup(x => x.GetByEmailAsync("zangeronimo@gmail.com"))
            .Returns(Task.FromResult(user));

        var passwordHasher = new Mock<IPasswordHasher>();
        passwordHasher
            .Setup(x => x.Verify("123", user.PasswordHash))
            .Returns(true);

        var permissionRepository = new Mock<IPermissionRepository>();
        permissionRepository
            .Setup(x => x.GetByUserId(userId))
            .ReturnsAsync(permissions);

        var tokenProvider = CreateProvider();

        var uc = new MakeLoginUC(
            userRepository.Object,
            passwordHasher.Object,
            permissionRepository.Object,
            tokenProvider
        );

        // Act
        var result = await uc.Execute("luciano", "123");

        // Assert
        result.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().NotBeNullOrEmpty();
    }
}
