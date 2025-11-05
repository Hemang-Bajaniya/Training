using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

public class Hash_Demo
{
    public static void Main()
    {
        string msg = "Hello 123";

        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            // for (int i = 0; i < msg.Length; i++)
            // {
            //     System.Console.WriteLine($"{msg[i]} maps to {bytes[i]}");
            // }

            var digest = sha.ComputeHash(bytes);

            string ip = Console.ReadLine();

            var computed = sha.ComputeHash(Encoding.UTF8.GetBytes(ip));
            if (digest.SequenceEqual(computed))
            {
                System.Console.WriteLine("Welcome");
            }
            else
            {
                System.Console.WriteLine("Wrong pass");
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in digest)
                sb.Append(b.ToString("x2"));

            System.Console.WriteLine(sb);

        }
    }
}