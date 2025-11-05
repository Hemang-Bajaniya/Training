public static class IntExtensionClass
{
    public static bool IsEven(this int a)
    {
        return a % 2 == 0;
    }
}
public static class EnumerableExtensions
{
    public static IEnumerable<T> TakeTopN<T>(this IEnumerable<T> source, int n, Func<T, int> selector)
    {
        return source.OrderByDescending(selector).Take(n);
    }
}
