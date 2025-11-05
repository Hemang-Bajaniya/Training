using System.Collections;
using System.Diagnostics;

namespace CollectionsDemo;

public class ArrayListDemo
{
    public static void Main()
    {
        List<int> list = new();
        ArrayList alist = new(10);

        alist.Add(10);
        alist.Add(10);
        alist.Add(10);
        alist.Add(10);
        alist.Add(10);
        alist.Add(10);

        System.Console.WriteLine($"{alist.Capacity}, {alist.Count} {alist.IsFixedSize}");


        Stopwatch sw = new();

        for (int i = 0; i < 1_000_0000; i++)
        {
            alist.Add(i);
        }

        sw.Start();
        alist.BinarySearch(55555);
        sw.Stop();
        System.Console.WriteLine(sw.ElapsedMilliseconds);

        sw.Reset();

        sw.Start();
        alist.IndexOf(55555);
        sw.Stop();
        System.Console.WriteLine(sw.ElapsedMilliseconds);

        System.Console.WriteLine(alist.Contains(7483));

        int[] arr = new int[10];
        alist.CopyTo(0, arr, 0, 10);
        System.Console.WriteLine(arr);

        foreach (var item in arr)
        {
            System.Console.WriteLine(item);
        }

        alist.Sort();

        alist.Remove(10);

        alist.RemoveRange(0, 10);

        alist.ToArray();


        // sw.Start();
        // for (int i = 0; i < 1_000_0000; i++)
        // {
        //     list.Add(i);
        // }
        // sw.Stop();
        // System.Console.WriteLine(sw.ElapsedMilliseconds);
    }
}