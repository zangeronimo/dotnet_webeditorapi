using System.Security.Cryptography;

using Nexora.Application.Interfaces;

namespace Nexora.Infrastructure.Provider;

public class SecretGenerator : ISecretGenerator
{
    public string Generate()
    {
        return $"nex_sec_{Convert.ToHexString(RandomNumberGenerator.GetBytes(32))}";
    }
}