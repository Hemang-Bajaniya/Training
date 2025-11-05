using System.Security.Cryptography;
using System.Text;

class RSA_Example
{
    public static void Main()
    {
        string s = "Password = 1234";
        using (RSA rsa = RSA.Create(2048))
        {
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            byte[] pubkey = rsa.ExportRSAPublicKey();
            byte[] pvtkey = rsa.ExportRSAPrivateKey();

            // System.Console.WriteLine("Pub key");
            // foreach (var item in pubkey)
            // {
            //     System.Console.Write(item);
            // }

            byte[] encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(s), RSAEncryptionPadding.OaepSHA256);
            string decrypted = Encoding.UTF8.GetString(rsa.Decrypt(encrypted, RSAEncryptionPadding.OaepSHA256));

            System.Console.WriteLine("Encrypted:");
            foreach (var item in encrypted)
            {
                System.Console.Write(item);
            }

            System.Console.WriteLine("\nDecrypted:");
            System.Console.WriteLine(decrypted);
        }
    }
}