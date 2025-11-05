namespace CollectionsDemo;

public class StackDemo
{
    public static void Main(string[] args)
    {
        Stack<int> s = new Stack<int>();
        s.Push(1);
        s.Push(2);
        s.Push(3);
        s.Push(4);

        int e;
        while (s.TryPop(out e))
        {
            System.Console.WriteLine(e);
        }

        s.Peek();
    }
}