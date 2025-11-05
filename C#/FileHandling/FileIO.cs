class Program
{
    static void Main()
    {
        string filePath = "./files/copy.txt";

        try
        {
            //Pros: Easy, no need to manage streams.
            // Cons: Loads everything into memory; not suitable for very large files.
            string content = File.ReadAllText(filePath);
            Console.WriteLine("File Content:");
            Console.WriteLine(content);

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
                System.Console.WriteLine(line);

            // using (StreamReader reader = new(filePath, System.Text.Encoding.UTF8))
            // {
            //     string line;
            //     while ((line = reader.ReadBlock(line, 1, 10)) != null)
            //         System.Console.WriteLine(line);
            // }

            foreach (string line in File.ReadLines(filePath))
            {
                Console.WriteLine(line);
            }

            // string content = "Hello, World!\nThis is a new line";
            // File.WriteAllText(filePath, content);
            // Console.WriteLine("File written successfully.");

            // string[] lines = { "Line 1", "Line 2", "Line 3" };
            // File.WriteAllLines(filePath, lines);

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                writer.WriteLine("Hello, World!");
                writer.Write("This is some text without a newline");
            }
        }

        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}