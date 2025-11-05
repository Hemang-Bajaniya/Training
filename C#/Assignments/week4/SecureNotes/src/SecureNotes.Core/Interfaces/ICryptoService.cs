namespace SecureNotes.Core.Interfaces;

public interface ICryptoService
{
    byte[] DeriveKey(string passphrase, byte[] salt, int keySize = 32);
    string Encrypt(string plainText, string pass);
    string Decrypt(string cipherText, string pass);
}