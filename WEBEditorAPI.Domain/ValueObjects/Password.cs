using WEBEditorAPI.Domain.Interfaces.Provider;

namespace WEBEditorAPI.Domain.ValueObjects;

public class Password
{
    public string Hash { get; private set; }
    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password Create(string password, IPasswordProvider provider)
    {
        var hash = provider.Generate(password);

        if (string.IsNullOrEmpty(hash))
        {
            throw new ArgumentException("Falha ao criar o Password");
        }
        return new Password(hash);
    }

    public static Password Restore(string hash)
    {
        return new Password(hash);
    }

    public Boolean Validate(string password, IPasswordProvider provider)
    {
        return provider.Validate(password, Hash);
    }
}
