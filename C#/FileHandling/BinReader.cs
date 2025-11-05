public class BinReader
{
    public static void Main()
    {
        using (FileStream stream = new FileStream("data.bin", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            writer.Write(42);          // Write int
            writer.Write(3.14);        // Write double
            writer.Write("Hello");     // Write string
            writer.Write(true);        // Write bool
        }

        using (FileStream stream = new FileStream("data.bin", FileMode.Open))
        using (BinaryReader reader = new(stream))
        {
            System.Console.WriteLine(reader.ReadInt32());
            System.Console.WriteLine(reader.ReadDouble());
        }
    }
}