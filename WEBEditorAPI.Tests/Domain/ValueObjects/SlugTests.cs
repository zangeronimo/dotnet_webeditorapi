using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Tests.Domain.ValueObjects;

public class SlugTests
{
    [Fact]
    public void Create_Should_Throw_Exception_When_Name_Is_Empty()
    {
        var exception = Assert.Throws<ArgumentException>(() => Slug.Create(""));

        Assert.Equal("Slug name não pode ser vazio.", exception.Message);
    }

    [Fact]
    public void Create_Should_Remove_Accents()
    {
        var slug = Slug.Create("Café");

        Assert.Equal("cafe", slug.Value);
    }

    [Fact]
    public void Create_Should_Convert_To_Lowercase()
    {
        var slug = Slug.Create("ARTIGO");

        Assert.Equal("artigo", slug.Value);
    }

    [Fact]
    public void Create_Should_Replace_Spaces_With_Dash()
    {
        var slug = Slug.Create("Aprendendo C Sharp");

        Assert.Equal("aprendendo-c-sharp", slug.Value);
    }

    [Fact]
    public void Create_Should_Remove_Special_Characters()
    {
        var slug = Slug.Create("Olá Mundo!!! .NET");

        Assert.Equal("ola-mundo-net", slug.Value);
    }

    [Fact]
    public void Create_Should_Normalize_Multiple_Dashes()
    {
        var slug = Slug.Create("Olá   Mundo");

        Assert.Equal("ola-mundo", slug.Value);
    }

    [Fact]
    public void Restore_Should_Keep_Value()
    {
        var slug = Slug.Restore("meu-artigo");

        Assert.Equal("meu-artigo", slug.Value);
    }
    [Fact]
    public void Slugs_With_Same_Value_Should_Be_Equal()
    {
        var slug1 = Slug.Create("Meu Artigo");
        var slug2 = Slug.Restore("meu-artigo");

        Assert.Equal(slug1.Value, slug2.Value);
    }
}
