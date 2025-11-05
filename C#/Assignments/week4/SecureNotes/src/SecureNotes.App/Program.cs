using SecureNotes.Infrastructure.Repositories;
using SecureNotes.Infrastructure.Security;
using SecureNotes.App.Menu;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Welcome to SecureNotes Vault");
Console.Write("Enter your passphrase: ");
var passphrase = ReadHiddenInput();

var vaultDir = Path.Combine(AppContext.BaseDirectory, "vault");
var crypto = new AesCryptoService();
var repository = new NotesRepo(vaultDir, passphrase, crypto);

var menu = new MainMenu(repository, crypto, passphrase);
await menu.StartAsync();

// --- Helper to hide password input ---
static string ReadHiddenInput()
{
    var pass = string.Empty;
    ConsoleKey key;
    do
    {
        var keyInfo = Console.ReadKey(intercept: true);
        key = keyInfo.Key;

        if (key == ConsoleKey.Backspace && pass.Length > 0)
        {
            pass = pass[..^1];
            Console.Write("\b \b");
        }
        else if (!char.IsControl(keyInfo.KeyChar))
        {
            pass += keyInfo.KeyChar;
            Console.Write("*");
        }
    } while (key != ConsoleKey.Enter);

    Console.WriteLine();
    return pass;
}
