public class Myclass
{
    public static void Main()
    {
        List<int> numbers = new List<int> { 1, 5, 3, 8, 2 };
        var topThree = numbers.TakeTopN(3, x => x);
        Console.WriteLine(string.Join(", ", topThree));
    }
}