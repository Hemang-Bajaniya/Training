using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SecureNotes.Infrastructure.Serialization;

public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private const string Format = "O";

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTimeOffset.Parse(reader.GetString() ?? DateTimeOffset.UtcNow.ToString(Format));

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToUniversalTime().ToString(Format));
}
