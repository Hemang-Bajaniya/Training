using System.Security.Cryptography;

public class AES_Example
{
    public static void Main()
    {
        var data = File.ReadAllLines("data.txt");
        foreach (var item in data)
        {
            System.Console.WriteLine(item);
        }

        using (Aes aes = Aes.Create())
        {
            byte[] encrypted = EncryptBytes(data[0], aes.Key, aes.IV);

            string decrypted = DecryptBytes(encrypted, aes.Key, aes.IV);

            System.Console.WriteLine($"\nOriginal: {data[0]}");

            System.Console.WriteLine("\nEncrypted");
            foreach (byte item in encrypted)
            {
                System.Console.Write(item);
            }

            System.Console.WriteLine($"\nDecrypted: {decrypted}");
        }
    }

    public static byte[] EncryptBytes(string plain, byte[] key, byte[] Iv)
    {
        if (plain == null || plain.Length <= 0)
            throw new ArgumentNullException("plain");
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException("key");
        if (Iv == null || Iv.Length <= 0)
            throw new ArgumentNullException("Iv");
        byte[] encrypted;

        using (Aes myAes = Aes.Create())
        {
            myAes.Key = key;
            myAes.IV = Iv;

            ICryptoTransform cryptoTransform = myAes.CreateEncryptor(myAes.Key, myAes.IV);

            using (MemoryStream memoryStream = new())
            {
                using (CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(cryptoStream))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plain);
                    }
                }

                encrypted = memoryStream.ToArray();
            }
        }
        return encrypted;
    }

    static string DecryptBytes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}