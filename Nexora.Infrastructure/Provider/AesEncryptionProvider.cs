using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Options;

using Nexora.Application.Interfaces;
using Nexora.Infrastructure.Options;

public class AesEncryptionProvider : IEncryptionProvider
{
    private readonly byte[] _key;

    public AesEncryptionProvider(IOptions<SecurityOptions> options)
    {
        _key = Convert.FromHexString(options.Value.EncryptionKey);
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();

        aes.Key = _key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();

        var plainBytes = Encoding.UTF8.GetBytes(plainText);

        var encryptedBytes =
            encryptor.TransformFinalBlock(
                plainBytes,
                0,
                plainBytes.Length);

        var result = new byte[aes.IV.Length + encryptedBytes.Length];

        Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
        Buffer.BlockCopy(
            encryptedBytes,
            0,
            result,
            aes.IV.Length,
            encryptedBytes.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText)
    {
        var fullCipher =
            Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();

        aes.Key = _key;

        var iv = new byte[aes.BlockSize / 8];

        Buffer.BlockCopy(
            fullCipher,
            0,
            iv,
            0,
            iv.Length);

        aes.IV = iv;

        var cipherBytes =
            new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(
            fullCipher,
            iv.Length,
            cipherBytes,
            0,
            cipherBytes.Length);

        using var decryptor =
            aes.CreateDecryptor();

        var plainBytes =
            decryptor.TransformFinalBlock(
                cipherBytes,
                0,
                cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }
}