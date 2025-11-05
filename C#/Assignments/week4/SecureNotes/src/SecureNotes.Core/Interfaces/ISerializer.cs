namespace SecureNotes.Core.Interfaces;

public interface ISerializer<T>
{
    string Serialize(IEnumerable<T> data);
    IEnumerable<T> Deserialize(string json);
}
