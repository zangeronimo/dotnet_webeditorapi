using WEBEditorAPI.Infrastructure.Provider;

namespace WEBEditorAPI.Tests.Infrastructure.Provider;

public class Argon2PasswordProviderTests
{
    private Argon2PasswordProvider MakeSut()
    {
        return new Argon2PasswordProvider();
    }

    [Fact]
    public void Generate_Should_Throw_Exception_When_Password_Is_Empty()
    {
        var sut = MakeSut();
        var exception = Assert.Throws<ArgumentException>(() => sut.Generate(""));

        Assert.Equal("Password não pode ser vazio.", exception.Message);
    }

    [Fact]
    public void Generate_Should_Return_A_Hash_With_Success()
    {
        var sut = MakeSut();
        var password = "test";
        var hash = sut.Generate(password);

        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.NotEqual(password, hash);

        var isValid = sut.Validate(password, hash);

        Assert.True(isValid);
    }
}
