using System.Reflection;

public class Post
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}

public class ReflectionDemo
{
    public static void Main()
    {
        Post post = new();
        Type type = typeof(Post);
        System.Console.WriteLine(type);

        foreach (var item in type.GetProperties())
            System.Console.WriteLine(item);

        System.Console.WriteLine(type.Name + type.Namespace + type.Assembly + type.BaseType + type.AssemblyQualifiedName);

        foreach (var item in type.GetMethods(BindingFlags.Public))
        {
            System.Console.WriteLine(item);
        }

        object p = Activator.CreateInstance(type);

        PropertyInfo propertyInfo = type.GetProperty("title");
        propertyInfo.SetValue(p, "New title");

        string title = propertyInfo.GetValue(p).ToString();
        System.Console.WriteLine(title);

    }
}