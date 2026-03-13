using System.Security.Cryptography;
using WEBEditorAPI.Domain.Interfaces.Provider;

namespace WEBEditorAPI.Infrastructure.Provider;

public class PBKDF2PasswordProvider : IPasswordProvider
{
    private const int SaltSize = 16; // 128 bits
    private const int KeySize = 32;  // 256 bits
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public (string Hash, string Salt) Generate(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        // novo método estático
        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: saltBytes,
            iterations: Iterations,
            hashAlgorithm: Algorithm,
            outputLength: KeySize
        );

        return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
    }

    public bool Validate(string password, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var expectedHashBytes = Convert.FromBase64String(hash);

        var computedHashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: saltBytes,
            iterations: Iterations,
            hashAlgorithm: Algorithm,
            outputLength: KeySize
        );

        return CryptographicOperations.FixedTimeEquals(computedHashBytes, expectedHashBytes);
    }
}