using Company;
using System.Collections.Generic;

namespace Company.Utils
{
    public class Printer
    {
        public static void PrintList<T>(string title, List<T> items)
        {
            System.Console.WriteLine($"\n----------------{title}----------------");

            foreach (var item in items)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
