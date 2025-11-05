using System.Dynamic;
using System.Text.Json;

public static class DynamicDemo
{
    public static void Run()
    {
        Console.WriteLine("Dynamic JSON example:");

        // Suppose JSON comes from an unknown external API
        string json = "{ \"RoomName\": \"Library A\", \"Seats\": 40 }";

        dynamic data = JsonSerializer.Deserialize<ExpandoObject>(json)!;
        Console.WriteLine($"Room: {data.RoomName}, Seats: {data.Seats}");

        // Strongly typed version
        var typed = JsonSerializer.Deserialize<RoomInfo>(json);
        Console.WriteLine($"(Typed) Room: {typed?.RoomName}, Seats: {typed?.Seats}");

        // Pitfall: misspelling property name
        try
        {
            Console.WriteLine(data.Roomname); // throws runtime binder exception
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Pitfall: {ex.Message}");
        }
    }
}

public class RoomInfo
{
    public string RoomName { get; set; } = "";
    public int Seats { get; set; }
}
