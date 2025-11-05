public class FileOp
{
    public static void Main()
    {
        string rootPath = @"./files";
        string filePath = Path.Join(rootPath, "new.txt");

        if (File.Exists(filePath))
        {
            // System.Console.WriteLine("File exist");
            // File.AppendAllText(filePath, "\nnew line1");
            // File.Copy(filePath, Path.Join(rootPath, "copy.txt")); // throws System.IO.IOException if file exist

            // File.Delete(filePath);

            var data = File.ReadAllText(filePath);
            System.Console.WriteLine(data);

            File.Replace(filePath, Path.Join(rootPath, "copy.txt"), Path.Join(rootPath, "backup.txt"));
        }
        else
        {
            File.Create(filePath);
        }

        System.Console.WriteLine("ok");
    }
}