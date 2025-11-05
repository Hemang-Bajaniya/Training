namespace CollectionsDemo;

public class DictDemo
{
    public static void Main()
    {
        Dictionary<char, int> fileInfo = new();

        foreach (var c in File.ReadAllText("text.txt"))
        {
            int cnt;
            if (fileInfo.TryGetValue(c, out cnt))
                if (fileInfo.ContainsKey(c))
                {
                    fileInfo[c] += 1;
                }
                else
                {
                    fileInfo[c] = 1;
                }
        }

        // foreach (var (c, v) in fileInfo)
        // {
        //     System.Console.WriteLine($"{c}:{v}");
        // }

        // foreach (KeyValuePair<char, int> kv in fileInfo)
        // {
        //     System.Console.WriteLine("k");
        //     System.Console.WriteLine($"{kv.Key}, {kv.Value}");
        // }

        var keys = fileInfo.Keys;
        var vals = fileInfo.Values;
    }
}