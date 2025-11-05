namespace DataTypes;

enum Colors
{
    red = 2,
    green = 1,
    blue = 3
}

class EnumDemo
{
    public static void Main()
    {
        System.Console.WriteLine("Enter color \n1-green\n2-red\n3-blue:");
        string color = Console.ReadLine();

        // if (int.TryParse(color, out int i))
        //     if (Enum.IsDefined((Colors)i))
        //         System.Console.WriteLine((Colors)i);

        if (int.TryParse(color, out int i))
        {
            // if (Enum.IsDefined(typeof(Colors), color))
            if (Enum.IsDefined(typeof(Colors), i))
                System.Console.WriteLine($"{i} is defined in colors");
            else
                System.Console.WriteLine($"{i} is not defined in colors");
        }

        // if (int.TryParse(color, out int c))
        // {
        //     System.Console.WriteLine("Int");
        //     System.Console.WriteLine(c == (int)Colors.green);
        // }
        // else if (Enum.TryParse(color, out Colors col))
        // {
        //     System.Console.WriteLine("Str");
        //     System.Console.WriteLine(col == Colors.green);
        // }
        // else 
        // {
        //     System.Console.WriteLine("Not a green");
        // }
    }
}