using System.Security.Cryptography;

namespace SecureNotes.Infrastructure.Security;

public static class SecureMemory
{
    public static void Zero(byte[] buffer)
    {
        if (buffer == null) return;
        CryptographicOperations.ZeroMemory(buffer);
    }
}
