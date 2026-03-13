using System;
using WEBEditorAPI.Domain.Interfaces.Provider;

namespace WEBEditorAPI.Domain.ValueObjects;

public class Password
{
    public string Hash { get; private set; }
    public string Salt { get; private set; }
    private Password(string hash, string salt)
    {
        Hash = hash;
        Salt = salt;
    }

    public static Password Create(string password, IPasswordProvider provider)
    {
        var (hash, salt) = provider.Generate(password);

        if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
        {
            throw new ArgumentException("Fail on create a new password.");
        }
        return new Password(hash, salt);
    }

    public static Password Restore(string hash, string salt)
    {
        return new Password(hash, salt);
    }

    public Boolean Validate(string password, IPasswordProvider provider)
    {
        return provider.Validate(password, Hash, Salt);
    }
}
