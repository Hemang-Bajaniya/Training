using System.Text.Json;
using System.Xml.Serialization;

Person person = new("Bob", 10);

var op = new JsonSerializerOptions();
op.PropertyNameCaseInsensitive = true;
op.WriteIndented = true;


string json = JsonSerializer.Serialize<Person>(person, op);
File.WriteAllText("person.json", json);

System.Console.WriteLine(json);

Person person1 = JsonSerializer.Deserialize<Person>(File.ReadAllText("person.json"));

System.Console.WriteLine(person1.Name);