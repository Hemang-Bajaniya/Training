## Project review: Access-Modifiers

This file summarizes the code, structure, findings, and suggested next steps for the `Access-Modifiers` project located at the workspace root.

### Repository layout (relevant files)

- `Access-Modifiers.csproj` — main project file (target: `net9.0`).
- `Program.cs` — primary demo code showing C# access modifiers and usage of `MyLibrary`.
- `Program2.cs` — a small `MyClass` with a `Main` that prints "Hello World" (duplicate entry point risk).
- `bin/`, `obj/` — build artifacts.

### What the project demonstrates

`Program.cs` (namespace `AccessModifiersDemo`) demonstrates the different accessibility levels in C#:

- public
- private
- protected
- internal
- protected internal

It also shows a derived class accessing inherited members and uses an external `MyLibrary` (via `using MyLibrary;` and a `ProjectReference`).

`Program2.cs` contains a small `MyClass` with a `Main` method that writes `Hello World`.

### Key findings and notes

- Namespace vs project RootNamespace
  - `Program.cs` uses `namespace AccessModifiersDemo` while the csproj `RootNamespace` is `Access_Modifiers` — this is not an error, but keeping them aligned improves clarity.

- Multiple `Main` methods / startup object
  - `Program2.cs` defines `public static void Main()` as does `Program.cs` (the latter has `static void Main(string[] args)`), which can lead to multiple entry points / ambiguity at build time.
  - The csproj contains a commented-out `<StartupObject>AccessModifiersDemo.Program</StartupObject>`. If you intend `Program.cs` to be the entry point, either remove `Program2.cs` or set `StartupObject` (or rename/remove the second `Main`).

- Access modifiers usage
  - The demo correctly shows what each modifier allows. Most classes in `Program.cs` are declared without an explicit access modifier (default internal). This is OK for an internal demo app.
  - If you intend to expose any types to other assemblies, mark them `public` explicitly.

- External library usage
  - The project references `..\MyLibrary\MyLibrary.csproj`. There is a `MyLibrary.dll` in `bin/Debug/net9.0/`. Ensure the referenced project path exists in your repo or that the binary is intentionally distributed.
  - `Program.cs` calls `Helper.GetDateTime()` and `Helper.Greet()` from `MyLibrary`. One commented line shows `Helper.FindSquareRoot(16)` is internal or unavailable — keep an eye on the intended accessibility of those members.

- Minor style / consistency items
  - Some console calls use `System.Console.WriteLine` while others use `Console.WriteLine` — pick one for consistency.
  - Consider making demo classes `public` if you plan to test or use them from outside the assembly.

### Build and run (Windows / cmd.exe)

To build the project from the workspace root (Windows cmd):

```
dotnet build "e:\RKIT\Pre_Joining_Training\C#\Access-Modifiers\Access-Modifiers.csproj"
```

To run the project (default startup) from the project directory:

```
cd "e:\RKIT\Pre_Joining_Training\C#\Access-Modifiers"
dotnet run
```

If there are multiple entry points and you want to force `Program.cs` as the startup object, either set the `<StartupObject>` in the csproj (fully-qualified type name) or remove the other `Main`.

