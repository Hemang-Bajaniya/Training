using System.Text;

namespace DataTypes;

public class Stringdemo
{
    public static void Main()
    {
        string s1 = "Hello";
        string? sn = null;

        string empty = System.String.Empty;
        empty = "Hello";

        string s = "print\\n";
        System.Console.WriteLine(s);
        s = @"\n";
        System.Console.WriteLine(s);

        // System.Console.WriteLine(s1 + sn + empty);

        string s3 = "Visual C#";
        System.Console.WriteLine(s3.Substring(7, 2));
        // Output: "C#"

        System.Console.WriteLine(s3.Replace("C#", "Basic"));
        // Output: "Visual Basic"

        // Index values are zero-based
        int index = s3.IndexOf("C");
        // index = 7

        StringBuilder stringBuilder = new("Hello C#");
        // stringBuilder.Capacity = 4;
        stringBuilder.Append("\nNew Line");
        stringBuilder.Insert("Hello".Length, " World of");
        stringBuilder.Remove("Hello".Length, 3);
        stringBuilder.Replace("Hello", "Bye");

        stringBuilder[2] = 'x';

        System.Console.WriteLine(stringBuilder);
    }
}