using System.Runtime.ConstrainedExecution;

namespace DataTypes;

class IntegralDemo
{
    public static void Main()
    {
        // int x = 100;
        // Int32 y = 100;

        // System.Console.WriteLine();

        // System.Console.WriteLine($"{Int32.MaxValue}, {Int32.MinValue}");
        // System.Console.WriteLine($"{int.MaxValue}, {int.MinValue}");
        // System.Console.WriteLine($"{nint.MaxValue}, {nint.MinValue}");
        // System.Console.WriteLine($"{nuint.MaxValue}, {nuint.MinValue}");

        System.Numerics.BigInteger bigInteger = new System.Numerics.BigInteger();

        bigInteger = (System.Numerics.BigInteger)Math.Pow(2, 100);
        System.Console.WriteLine(bigInteger);

        // Decimal d = (Decimal)Math.Pow(2, 100);
        // System.Console.WriteLine(d);

        var decimalLiteral = 42;
        var hexLiteral = 0x2A;
        var binaryLiteral = 0b_0010_1010;
        System.Console.WriteLine($"{decimalLiteral} {hexLiteral} {binaryLiteral}");

        sbyte b = -100;
        byte by = (byte)b;
        System.Console.WriteLine($"{b}, {by}");
    }
}