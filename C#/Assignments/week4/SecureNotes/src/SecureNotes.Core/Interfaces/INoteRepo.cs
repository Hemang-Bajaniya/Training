using SecureNotes.Core.Models;

namespace SecureNotes.Core.Interfaces;

// Abstracts how notes are persisted — file, DB, or any storage.
public interface INoteRepo
{
    Task<IEnumerable<Note>> GetAllAsync();
    Task<Note?> GetNoteById(Guid guid);
    Task AddAsync(Note note);
    Task UpdateAsync(Note note);
    Task DeleteAsync(Guid guid);
}