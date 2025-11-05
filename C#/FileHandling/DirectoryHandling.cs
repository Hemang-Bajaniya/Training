public class DirectoryDemo
{
    public static void Main(string[] args)
    {
        string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "files");

        try
        {
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
                Console.WriteLine($"Created directory: {rootPath}");
            }

            string myDirPath = Path.Combine(rootPath, "MyDir");

            if (!Directory.Exists(myDirPath))
            {
                Directory.CreateDirectory(myDirPath);
                Console.WriteLine($"Created directory: {myDirPath}");
            }
            else
            {
                Console.WriteLine($"Files in {myDirPath}:");
                string[] files = Directory.GetFiles(myDirPath);
                if (files.Length == 0)
                {
                    Console.WriteLine("No files found in the directory.");
                }
                else
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine($"{Path.GetFileNameWithoutExtension(file)} with ext {Path.GetExtension(file)}");
                    }
                }

                // Directory.Delete(myDirPath, true);
                // Directory.Move(myDirPath, rootPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }
}