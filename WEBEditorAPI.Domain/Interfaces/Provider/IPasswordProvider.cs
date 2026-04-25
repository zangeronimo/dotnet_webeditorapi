using System;

namespace WEBEditorAPI.Domain.Interfaces.Provider;

public interface IPasswordProvider
{
    string Generate(string password);
    bool Validate(string password, string hash);
}
