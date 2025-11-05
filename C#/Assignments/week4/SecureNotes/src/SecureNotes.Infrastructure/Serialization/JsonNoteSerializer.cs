using System.Collections.Generic;
using System.Text.Json;
using SecureNotes.Core.Interfaces;
using SecureNotes.Core.Models;

namespace SecureNotes.Infrastructure.Serialization;

public class JsonNoteSerializer : ISerializer<Note>
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters = { new DateTimeOffsetConverter() }
    };

    public string Serialize(IEnumerable<Note> data)
        => JsonSerializer.Serialize(data, _options);

    public IEnumerable<Note> Deserialize(string json)
        => string.IsNullOrWhiteSpace(json)
            ? new List<Note>()
            : JsonSerializer.Deserialize<List<Note>>(json, _options) ?? new List<Note>();
}
