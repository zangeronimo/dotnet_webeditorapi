using FluentAssertions;
using Microsoft.Extensions.Options;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Infrastructure.Options;
using WEBEditorAPI.Infrastructure.Provider;

namespace WEBEditorAPI.Tests.Infrastructure.Provider;

public class JwtProviderTests
{
    private JwtProvider MakeSut()
    {
        var jwtOptions = new JwtOptions
        {
            Secret = "mysuperlongsecretkeywith32chars!!",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationSeconds = 15,
            RefreshExpirationHours = 24
        };
        var options = Options.Create(jwtOptions);
        return new JwtProvider(options);
    }

    [Fact]
    public void Should_Generate_And_Validate_Token_With_Permissions()
    {
        // Arrange
        var provider = MakeSut();

        var userId = Guid.NewGuid();
        var companyId = Guid.NewGuid();
        var username = "zangeronimo";

        var permissions = new List<string>
        {
            "product.read",
            "product.write"
        };

        // Act
        var token = provider.GenerateToken(
            userId,
            username,
            permissions,
            companyId,
            TokenType.Access
        );

        var payload = provider.ValidateAccessToken(token);

        // Assert
        payload.UserId.Should().Be(userId);
        payload.CompanyId.Should().Be(companyId);
        payload.Permissions.Should().BeEquivalentTo(permissions);
    }

    [Fact]
    public void Should_Not_Allow_Refresh_Token_As_Access_Token()
    {
        // Arrange
        var provider = MakeSut();

        var userId = Guid.NewGuid();
        var companyId = Guid.NewGuid();

        var refreshToken = provider.GenerateToken(
            userId,
            "zangeronimo",
            new List<string> { "product.read" },
            companyId,
            TokenType.Refresh
        );

        // Act
        Action act = () => provider.ValidateAccessToken(refreshToken);

        // Assert
        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Should_Allow_Access_Token_As_Access_Token()
    {
        var provider = MakeSut();

        var token = provider.GenerateToken(
            Guid.NewGuid(),
            "luciano",
            new List<string> { "product.read" },
            Guid.NewGuid(),
            TokenType.Access
        );

        var payload = provider.ValidateAccessToken(token);

        payload.Should().NotBeNull();
    }
}
