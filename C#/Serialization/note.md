# Notes on Serialization in C#

## What is Serialization?
Serialization is the process of converting an in-memory object into a format that can be persisted (file, DB) or transmitted (network). Deserialization reconstructs the object from that format.

## Common Formats / Serializers
- JSON
  - System.Text.Json (built-in, high-performance, .NET Core+)
  - Newtonsoft.Json (Json.NET) — feature-rich, widely used
- XML
  - XmlSerializer
  - DataContractSerializer (good for WCF/data contracts)
- Binary
  - BinaryFormatter (obsolete/insecure — avoid)
  - Custom binary formats or protobuf/net (Protocol Buffers) for compactness and speed

## Attributes & Interfaces
- [Serializable] — marks a class for binary serializers (legacy).
- [NonSerialized] — applied to fields to skip them in binary serialization.
- ISerializable — implement for custom binary serialization logic (legacy pattern).
- [DataContract] / [DataMember] — used with DataContractSerializer.
- [JsonIgnore], [JsonPropertyName] — used with JSON serializers.

## Basic Examples

System.Text.Json (serialize / deserialize):
```csharp
// Json example
var obj = new Person { Name = "A", Age = 30 };
string json = System.Text.Json.JsonSerializer.Serialize(obj);
Person p2 = System.Text.Json.JsonSerializer.Deserialize<Person>(json);
```

DataContractSerializer:
```csharp
// Requires [DataContract] on type and [DataMember] on members
var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(Person));
using var stream = new FileStream("p.xml", FileMode.Create);
serializer.WriteObject(stream, obj);
```

ISerializable (custom):
```csharp
[Serializable]
class MyType : ISerializable
{
    public string Name;
    public MyType() { }
    protected MyType(SerializationInfo info, StreamingContext ctx)
    {
        Name = info.GetString("Name");
    }
    public void GetObjectData(SerializationInfo info, StreamingContext ctx)
    {
        info.AddValue("Name", Name);
    }
}
```

## Versioning & Compatibility
- Prefer explicit names for members (e.g., DataMember Order/Name) when versioning matters.
- For JSON, tolerate missing fields (deserializers typically ignore extras).
- Add nullable/new optional members rather than removing existing ones.

## Security Considerations
- Never deserialize untrusted data with insecure deserializers (BinaryFormatter and TypeNameHandling in Newtonsoft can be exploited).
- Prefer safe formats like JSON or explicit DataContractSerializer usage.
- Validate input after deserialization.

## Performance & Size
- JSON (System.Text.Json) is generally fast and compact for text.
- Binary/protobuf is faster and smaller for large data sets or high-throughput scenarios.
- Avoid reflection-heavy serializers in tight loops; consider source-gen (System.Text.Json source generation) for performance.

## Best Practices
- Use System.Text.Json for general JSON needs; use Newtonsoft only when advanced features are required.
- Avoid BinaryFormatter; use safer alternatives.
- Mark only necessary members for serialization (principle of least privilege).
- Sanitize and validate deserialized objects.
- Consider DTOs (data transfer objects) separate from domain models to avoid leaking internal state.

## Useful Links / APIs
- System.Text.Json.JsonSerializer
- System.Xml.Serialization.XmlSerializer
- System.Runtime.Serialization.DataContractSerializer
- protobuf-net for Protobuf support
