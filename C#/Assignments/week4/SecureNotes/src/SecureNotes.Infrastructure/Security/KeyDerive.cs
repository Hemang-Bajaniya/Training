using System.Security.Cryptography;

namespace SecureNotes.Infrastructure.Security;

public static class KeyDerivation
{
    public static byte[] DeriveKey(string passphrase, byte[] salt, int keySize = 32, int iterations = 100_000)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(keySize);
    }
}
