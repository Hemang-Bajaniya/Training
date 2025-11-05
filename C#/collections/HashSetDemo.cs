namespace CollectionsDemo;

class HashSetDemo
{
    public static void Main()
    {
        HashSet<int> hs = new HashSet<int>();

        hs.Add(10);
        hs.Add(20);
        hs.Add(30);
        hs.Add(10);

        HashSet<int> hs2 = new HashSet<int> { 10, 20, 30, 100, 200 };


        Console.WriteLine("Elements in the HashSet: ");
        foreach (int number in hs)
            Console.WriteLine(number);

        // foreach (var val in hs.Union(hs2))
        foreach (var val in hs.Intersect(hs2))
            System.Console.WriteLine(val);
    }
}