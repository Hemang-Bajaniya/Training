namespace DataTypes;

class CharDemo
{
    public static void Main()
    {
        var chars = new[]
    {
        'H',
        '\u0068',
        '\x0068',
        (char)104,
    };
        Console.WriteLine(string.Join(" ", chars));
    }
}