// string rootPath = @"./";

// string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);

// System.Console.WriteLine($"Total:{dirs.Length}");
// foreach (var dir in dirs)
//     System.Console.WriteLine(dir);

// Total:3
// ./bin
// ./files
// ./obj

// string[] files = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);
// foreach (var file in files)
// {
//     // System.Console.WriteLine(file);
//     // System.Console.WriteLine(Path.GetExtension(file));
//     // System.Console.WriteLine(Path.GetFileName(file));
//     // System.Console.WriteLine(Path.GetDirectoryName(file));
//     // System.Console.WriteLine(Path.GetRandomFileName(file));

//     // FileInfo fileInfo = new(file);
//     // System.Console.WriteLine($"{fileInfo.CreationTime} {fileInfo.Length} {fileInfo.Extension} {fileInfo.IsReadOnly} {fileInfo.LinkTarget}");
// }

// System.Console.WriteLine(Directory.Exists("./files"));

