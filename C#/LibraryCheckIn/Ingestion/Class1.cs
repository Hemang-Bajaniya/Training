namespace LibraryCheckIn.Ingestion;

public abstract class FileImporter<T>
{
    public abstract IEnumerable<T> Import(string path);
}
