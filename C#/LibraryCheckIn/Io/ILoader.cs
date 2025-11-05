using System.Data;

namespace LibraryCheckIn.Io;

public interface ILoader
{
    DataTable LoadIntoDataTable(string fileName);
}