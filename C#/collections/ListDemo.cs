namespace CollectionsDemo;

class ListDemo
{
    public static void Main()
    {
        List<int> ints = [];

        System.Console.WriteLine(ints.Count());
        System.Console.WriteLine(ints.Capacity);

        ints.Add(10);
        ints.Add(50);
        ints.Add(40);
        ints.Add(30);
        ints.Add(10);


        System.Console.WriteLine(ints.Count());
        System.Console.WriteLine(ints.Capacity);
        System.Console.WriteLine();

        ints.Sort((a, b) => a.CompareTo(b));

        ints.Reverse();

        ints.AddRange([10, 20, 40]);

        ints.Remove(10);

        // ints.RemoveAll((e) => (e % 2 == 0));

        ints.Insert(2, 430);

        System.Console.WriteLine(ints.Count());
        System.Console.WriteLine(ints.Capacity);
        System.Console.WriteLine();

        ints.Insert(2, 430);


        System.Console.WriteLine(ints.Count());
        System.Console.WriteLine(ints.Capacity);
        System.Console.WriteLine();

        // var l = ints.FindAll((e) => e == 10);

        // foreach (var item in l)
        // {
        //     System.Console.WriteLine(item);
        // }

        foreach (var item in ints)
        {
            System.Console.WriteLine(item);
        }
    }
}