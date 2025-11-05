using System.Collections;

namespace CollectionsDemo;

public static class LeaderboardUtils
{
    public static string GetTopScorer(SortedList<string, int> scores) =>
        scores.Any() ? scores.Keys[scores.Count - 1] : null;
}

public class BitArrayDemo
{
    public static void Main(string[] args)
    {
        BitArray bitArray = new(10, true);
        // List<
        foreach (var item in bitArray)
        {
            System.Console.WriteLine(item);
        }

        BitArray bits1 = new BitArray(new bool[] { true, false, true, true });
        BitArray bits2 = new BitArray(new bool[] { false, true, true });

        BitArray andResult = bits1.And(bits2); // AND operation
        BitArray orResult = bits1.Or(bits2);   // OR operation
        BitArray xorResult = bits1.Xor(bits2); // XOR operation
        BitArray notResult = bits1.Not();      // NOT operation

        for (int i = 0; i < andResult.Length; i++)
            Console.Write(andResult[i] ? "1" : "0");

        bits1.Set(1, true);
        bits1.SetAll(false);

        SortedList<string, int> leaderboard = new SortedList<string, int>
    {
        { "Alice", 90 }, { "Bob", 85 }, { "Charlie", 95 }
    };

        leaderboard["Jhon"] = 100;

        System.Console.WriteLine(LeaderboardUtils.GetTopScorer(leaderboard));
    }



}