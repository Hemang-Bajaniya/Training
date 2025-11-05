namespace DataTypes;

class FloatingDemo
{
    public static void Main()
    {
        float f = 22 / 7f;
        double d = 22 / 7d;
        decimal dc = 22 / 7m;

        System.Console.WriteLine($"{f} {d} {dc}");

        System.Console.WriteLine($"{double.MaxValue} {decimal.MaxValue}");

        // Double.PositiveInfinity;
        // Double.NaN;
        // Double.NegativeInfinity;

        decimal x = 0.1m;
        d = 0.2d;

        System.Console.WriteLine($"{x + (decimal)d}");
        System.Console.WriteLine($"{(double)x + d}");

        d = 0.42e2;
        Console.WriteLine(d);  // output 42

        f = 134.45E-2f;
        Console.WriteLine(f);  // output: 1.3445

        decimal m = 1.5E6m;
        Console.WriteLine(m);  // output: 1500000

        d = -1000009099000.32323d;
        int i;
        // System.Console.WriteLine(d);
        unchecked
        {
            i = (int)d;
            System.Console.WriteLine(i);
        }

        i = (int)d;
        System.Console.WriteLine(i);
    }
}