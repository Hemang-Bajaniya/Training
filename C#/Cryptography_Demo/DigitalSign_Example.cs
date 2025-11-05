using System.Security.Cryptography;
using System.Text;

public class DigitalSign_Demo
{
    public static void Main()
    {
        string msg = "Message from A";

        using (RSA rsa = RSA.Create(2048))
        {
            byte[] pubKey = rsa.ExportRSAPublicKey();
            byte[] pvtKey = rsa.ExportRSAPrivateKey();

            // byte[] bytes = Encoding.UTF8.GetBytes(msg);
            byte[] bytes = Encoding.UTF8.GetBytes(msg.Replace("A", "B"));

            // hash data -> (sha256) -> digest -> padding -> pvt key
            byte[] sign = rsa.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            bytes = Encoding.UTF8.GetBytes(msg);
            bool isOriginal = rsa.VerifyData(bytes, sign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            if (isOriginal)
            {
                System.Console.WriteLine("\nMessage is from aunthenticated source");
            }
            else
            {
                System.Console.WriteLine("\nMessage was changed during transmission");
            }
        }
    }
}