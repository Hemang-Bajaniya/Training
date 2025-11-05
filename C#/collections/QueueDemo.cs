using System.Runtime.ConstrainedExecution;

namespace CollectionsDemo;

public class QueueDemo
{
    public static void Main()
    {
        Queue<string> q = new();

        q.Enqueue("a");
        q.Enqueue("b");
        q.Enqueue("c");
        q.Enqueue("d");
        q.Enqueue("e");

        System.Console.WriteLine(q.Dequeue());

        System.Console.WriteLine(q.Peek());

        System.Console.WriteLine($"{q.Capacity} {q.Count}");

        string[] arr = q.ToArray();
        // q.Clear();
        // q.Dequeue();
        string res;
        if (q.TryDequeue(out res))
            System.Console.WriteLine(res);
    }
}