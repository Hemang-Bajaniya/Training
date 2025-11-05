using System.Text;
using SecureNotes.Core.Interfaces;
using SecureNotes.Core.Models;
using SecureNotes.Infrastructure.Serialization;

namespace SecureNotes.Infrastructure.Repositories;

public class NotesRepo : INoteRepo
{
    private readonly string _vaultPath;
    private readonly ISerializer<Note> _serializer;
    private readonly ICryptoService _crypto;
    private readonly byte[] _key;
    private readonly string _pass;

    public NotesRepo(string vaultDirectory, string passphrase, ICryptoService crypto)
    {
        _vaultPath = Path.Combine(vaultDirectory, "notes.json");
        _serializer = new JsonNoteSerializer();
        _crypto = crypto;
        _pass = passphrase;

        Directory.CreateDirectory(vaultDirectory);

        // Derive key once per session
        var salt = Encoding.UTF8.GetBytes("1213244");
        _key = _crypto.DeriveKey(passphrase, salt);
    }

    private async Task<List<Note>> LoadAsync()
    {
        if (!File.Exists(_vaultPath))
            return new List<Note>();

        try
        {
            var json = await File.ReadAllTextAsync(_vaultPath);
            return _serializer.Deserialize(json).ToList();
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Corrupted File!!");
            throw;
        }
    }

    private async Task SaveAsync(IEnumerable<Note> notes)
    {
        var json = _serializer.Serialize(notes);
        await File.WriteAllTextAsync(_vaultPath, json);
    }

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await LoadAsync();
    }

    public async Task<Note?> GetNoteById(Guid guid)
    {
        var notes = await LoadAsync();
        return notes.FirstOrDefault(n => n.Id == guid);
    }

    public async Task AddAsync(Note note)
    {
        var notes = await LoadAsync();
        note.Id = Guid.NewGuid();
        note.CreatedAt = DateTimeOffset.UtcNow;

        note.Body = _crypto.Encrypt(note.Body, _pass);
        notes.Add(note);
        await SaveAsync(notes);
    }

    public async Task UpdateAsync(Note updated)
    {
        var notes = await LoadAsync();
        var existing = notes.FirstOrDefault(n => n.Id == updated.Id);
        if (existing == null) return;

        existing.Title = updated.Title;
        existing.Body = _crypto.Encrypt(updated.Body, _pass);
        existing.UpdatedAt = DateTimeOffset.UtcNow;
        await SaveAsync(notes);
    }

    public async Task DeleteAsync(Guid id)
    {
        var notes = await LoadAsync();
        var filtered = notes.Where(n => n.Id != id).ToList();
        await SaveAsync(filtered);
    }


}