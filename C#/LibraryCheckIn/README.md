## LibraryCheckIn — Overview

This repository contains a small console application and supporting class libraries to process returned books for a tiny community library. The tool imports return records (CSV, with JSON support planned), maps them to domain objects, computes a small penalty score per return based on condition, and produces a daily summary text report.

Goals in this README:
- Explain the architecture with a short ASCII diagram.
- Describe the domain model and where types live.
- Explain design choices (abstract vs sealed, access modifiers).
- Show how to run the console app and where outputs appear.
- Describe tests and sample inputs/outputs.

## Architecture Diagram

```text
┌────────────────────────────┐
│        LibraryApp          │
│        (Console CLI)       │
└────────────┬───────────────┘
             │
             │ References: Domain, Io, Extensions, Ingestion
             ▼
        Scans ./In recursively
             │
             ▼
 ┌────────────────────────────┐
 │    Ingestion Pipeline      │
 │   (FileImporter<T> base)   │
 └────────────┬───────────────┘
              │
         ┌────┴────┐
         │         │
         ▼         ▼
 CsvBookImporter  JsonBookImporter (Io namespace)
         │
         ▼
 Produces IEnumerable<Book> → Processor
         │
         ▼
 Uses IReportWriter implementations:
     ├─ TextReportWriter
     └─ XmlReportWriter 

```

Project map (main files)
- Domain/
  - Book.cs (Book domain type and BookCondition enum)
  - BookDTO.cs (simple mapping DTO used by importers)
  - BookProcessor.cs (computes penalty, summaries and aggregations)
- Io/
  - CsvBookImporter.cs (imports CSV into DataTable then maps to Book)
  - JsonBookImporter.cs (imports JSON -> Book)  (stretch goal / strategy example)
  - TextReportWriter.cs / XmlReportWriter.cs
- LibraryApp/
  - Program.cs (CLI host, scanning and wiring pipeline)
- In/ (example inputs)
- out/ (reports generated)

## Domain model

- enum BookCondition { New, Good, Worn, Damaged }
- class Book
  - Properties: Id (string), Title (string), Author (string), Condition (BookCondition), Penalty (int?)

Notes on mapping: importers parse CSV/JSON rows into a BookDTO (simple primitive holder) then map to Book instances using a mapper (`Io/BookMapper.cs`). This keeps parsing concerns separate from domain logic.

Contract (small):
- Input: CSV file named `returns_YYYYMMDD.csv` with columns at minimum: Id, Title, Author, Condition
- Output: `./out/daily_summary_YYYYMMDD.txt` containing processing timestamp, total returns, counts by condition, and top 5 titles by penalty
- Error modes: missing file, missing columns, invalid enum strings -> friendly error and non-crashing failure for that file (logged/summarized)

## Penalty calculation

Penalty base adjustments (applied per returned book):
- Damaged: +10
- Worn: +3
- Good: 0
- New: -1

After calculating the penalty (base + any other math/weights), results are clamped to the inclusive range [0, 100]. The penalty is stored on the Book during processing to aid sorting and reporting.

Example summary line for a book: `123,The Hobbit,J.R.R. Tolkien,Worn -> "The Hobbit" by J.R.R. Tolkien (Penalty: 3)`

## Access modifier decisions (justified)

1) Book properties are public auto-properties (public string Title { get; set; }) while backing fields are not exposed. Reason: `Book` is a simple DTO-like domain model used across assemblies (Domain, Io, LibraryApp). Public properties make mapping and serialization straightforward while hiding backing storage details.

2) Helper/internal methods used only inside the `Domain` assembly (e.g., internal static validation helpers) are marked `internal` instead of `public`. Reason: reduces public API surface and prevents other assemblies from depending on internal helpers, enabling safer refactoring. See comment in `Domain/BookProcessor.cs` for an example.

Additional justification examples are present in code comments where `private` fields and `internal` members are used.

## Abstract vs sealed (design rationale)

- Abstract: `FileImporter<T>` is modeled as `abstract` because we expect multiple format-specific implementations (CSV, JSON, XML, etc.). It declares the contract `IEnumerable<T> Import(string path)` and provides optional protected helpers for common import steps (file reading, validation). Marking it `abstract` encourages extension via derived classes.

- Sealed: Concrete report writers like `TextReportWriter` and `XmlReportWriter` are marked `sealed` where further extension is not expected or desirable. For example, `TextReportWriter` is a small final implementation that writes a deterministic text file; sealing it avoids accidental inheritance and simplifies reasoning about behavior. Each sealed class includes an XML doc comment explaining the decision.

## File & IO behavior

- Input file pattern: `returns_YYYYMMDD.csv` (CSV) — located anywhere under the `In/` directory. The host scans `./In` recursively.
- Output directory: configurable; default is `./out`. Reports are named `daily_summary_YYYYMMDD.txt`.
- Processing timestamp format: `yyyy-MM-dd HH:mm:ss` (local time).
- Friendly error messages are printed to console. File-level exceptions are caught and summarized; the host will continue processing other files unless a fatal error occurs.

## Sample output (daily_summary_20250929.txt)

Date processed: 2025-09-29 14:02:11
Total returns: 123
Counts by condition:
- New: 5
- Good: 80
- Worn: 30
- Damaged: 8

Top 5 titles by penalty (desc):
1. 987,Old Book,Jane Author, Damaged -> "Old Book" by Jane Author (Penalty: 10)
2. 543,Another Worn Title,John Doe, Worn -> "Another Worn Title" by John Doe (Penalty: 3)
3. ...

## How to build & run

From the repo root (Windows / cmd.exe):

1) Build

```cmd
dotnet build LibraryCheckIn.sln
```

2) Run the console app (default scans ./In and writes to ./out)

```cmd
dotnet run --project LibraryApp
```

3) Dry-run (no files written, shows what would be done)

```cmd
dotnet run --project LibraryApp --dry-run
```

## Sample input files

Place sample CSVs in `LibraryApp/In/` or top-level `In/`. Example header:

Id,Title,Author,Condition
123,The Hobbit,J.R.R. Tolkien,Worn
456,Don Quixote,Miguel de Cervantes,Damaged

## Stretch goals implemented / planned

- Accept both CSV and JSON inputs via an importer strategy pattern (implemented by `FileImporter<T>` derivatives). The host selects importer by extension.
- Add a simple `appsettings.json` or `config.json` to configure the default output directory and behaviour (planned).
