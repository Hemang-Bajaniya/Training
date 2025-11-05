using System.Security.Cryptography;
using System.Text;
using SecureNotes.Core.Interfaces;
using SecureNotes.Infrastructure.Security;

namespace SecureNotes.Infrastructure.Security;

public class AesCryptoService : ICryptoService
{
    public byte[] DeriveKey(string passphrase, byte[] salt, int keySize = 32)
        => KeyDerivation.DeriveKey(passphrase, salt, keySize);

    public string Encrypt(string plainText, string passphrase)
    {
        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        // Generate salt and IV
        var salt = RandomNumberGenerator.GetBytes(16);
        var iv = RandomNumberGenerator.GetBytes(16);

        // Derive key using PBKDF2
        using var keyDerive = new Rfc2898DeriveBytes(passphrase, salt, 100_000, HashAlgorithmName.SHA256);
        var key = keyDerive.GetBytes(32);

        using var encryptor = aes.CreateEncryptor(key, iv);
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        // Combine salt + IV + ciphertext
        var combined = new byte[salt.Length + iv.Length + cipherBytes.Length];
        Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
        Buffer.BlockCopy(iv, 0, combined, salt.Length, iv.Length);
        Buffer.BlockCopy(cipherBytes, 0, combined, salt.Length + iv.Length, cipherBytes.Length);

        SecureMemory.Zero(key);
        SecureMemory.Zero(plainBytes);

        return Convert.ToBase64String(combined);
    }


    public string Decrypt(string cipherText, string passphrase)
    {
        var combined = Convert.FromBase64String(cipherText);

        var salt = new byte[16];
        var iv = new byte[16];
        Buffer.BlockCopy(combined, 0, salt, 0, salt.Length);
        Buffer.BlockCopy(combined, salt.Length, iv, 0, iv.Length);

        var cipherBytes = new byte[combined.Length - salt.Length - iv.Length];
        Buffer.BlockCopy(combined, salt.Length + iv.Length, cipherBytes, 0, cipherBytes.Length);

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        // Derive key using the same salt
        using var keyDerive = new Rfc2898DeriveBytes(passphrase, salt, 100_000, HashAlgorithmName.SHA256);
        var key = keyDerive.GetBytes(32);

        using var decryptor = aes.CreateDecryptor(key, iv);
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        var result = Encoding.UTF8.GetString(plainBytes);
        SecureMemory.Zero(key);
        SecureMemory.Zero(plainBytes);

        return result;
    }

}
