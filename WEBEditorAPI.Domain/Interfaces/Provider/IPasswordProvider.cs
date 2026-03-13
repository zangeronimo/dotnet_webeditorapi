using System;

namespace WEBEditorAPI.Domain.Interfaces.Provider;

public interface IPasswordProvider
{
    (string Hash, string Salt) Generate(string password);
    bool Validate(string password, string hash, string salt);
}
