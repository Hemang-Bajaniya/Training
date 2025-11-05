using SecureNotes.Core.Interfaces;
using SecureNotes.Core.Models;
using SecureNotes.Infrastructure.Security;

namespace SecureNotes.App.Menu;

public class MainMenu
{
    private readonly INoteRepo _repo;
    private readonly ICryptoService _crypto;
    private readonly byte[] _key;
    private readonly string _pass;

    public MainMenu(INoteRepo repo, ICryptoService crypto, string passphrase)
    {
        _repo = repo;
        _crypto = crypto;
        var salt = System.Text.Encoding.UTF8.GetBytes("SecureNotesFixedSalt");
        // _key = _crypto.DeriveKey(passphrase, salt);
        _pass = passphrase;
    }

    public async Task StartAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("SecureNotes Menu");
            Console.WriteLine("1. Create Note");
            Console.WriteLine("2. View All Notes");
            Console.WriteLine("3. View Note by ID");
            Console.WriteLine("4. Update Note");
            Console.WriteLine("5. Delete Note");
            Console.WriteLine("6. Exit");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": await CreateNoteAsync(); break;
                case "2": await ViewAllNotesAsync(); break;
                case "3": await ViewNoteAsync(); break;
                case "4": await UpdateNoteAsync(); break;
                case "5": await DeleteNoteAsync(); break;
                case "6": exit = true; break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private async Task CreateNoteAsync()
    {
        Console.Write("Title: ");
        var title = Console.ReadLine() ?? string.Empty;
        Console.Write("Body: ");
        var body = Console.ReadLine() ?? string.Empty;

        var note = new Note { Title = title, Body = body };
        await _repo.AddAsync(note);
        Console.WriteLine("Note created successfully.");
    }

    private async Task ViewAllNotesAsync()
    {
        var notes = await _repo.GetAllAsync();
        if (!notes.Any())
        {
            Console.WriteLine(" No notes found.");
            return;
        }

        Console.WriteLine("\nNotes:");
        foreach (var n in notes)
        {
            Console.WriteLine($"• {n.Id} | {n.Title} | Created: {n.CreatedAt:g}");
        }
    }

    private async Task ViewNoteAsync()
    {
        Console.Write("Enter Note ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var note = await _repo.GetNoteById(id);
        if (note == null)
        {
            Console.WriteLine("Note not found.");
            return;
        }

        try
        {
            var decrypted = _crypto.Decrypt(note.Body, _pass);
            Console.WriteLine($"\n{note.Title}\n{new string('-', note.Title.Length)}");
            Console.WriteLine(decrypted);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            Console.WriteLine("Failed to decrypt note. Wrong key or corrupt data.");
        }
    }

    private async Task UpdateNoteAsync()
    {
        Console.Write("Enter Note ID to update: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var existing = await _repo.GetNoteById(id);
        if (existing == null)
        {
            Console.WriteLine("Note not found.");
            return;
        }

        Console.Write("New Title: ");
        var title = Console.ReadLine() ?? existing.Title;
        Console.Write("New Body: ");
        var body = Console.ReadLine() ?? string.Empty;

        existing.Title = title;
        existing.Body = body;
        await _repo.UpdateAsync(existing);

        Console.WriteLine("Note updated.");
    }

    private async Task DeleteNoteAsync()
    {
        Console.Write("Enter Note ID to delete: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        await _repo.DeleteAsync(id);
        Console.WriteLine("Note deleted.");
    }
}
