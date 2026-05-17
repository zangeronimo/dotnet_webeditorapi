using Isopoh.Cryptography.Argon2;
using Nexora.Domain.Interfaces.Provider;

namespace Nexora.Infrastructure.Provider;

public class Argon2PasswordProvider : IPasswordProvider
{
    public string Generate(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password não pode ser vazio.");

        return Argon2.Hash(password);
    }

    public bool Validate(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
            return false;

        return Argon2.Verify(hash, password);
    }
}