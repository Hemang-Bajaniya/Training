using System.Data;

namespace LibraryCheckIn.Io
{
    public class CsvLoader : ILoader
    {
        public DataTable LoadIntoDataTable(string fileName)
        {
            DataTable dt = new();

            if (!File.Exists(fileName))
            {
                System.Console.WriteLine($"{fileName} not exsist");
                return dt;
            }

            try
            {
                string[] lines = File.ReadAllLines(fileName);
                string[] columns = lines[0].Split(",");

                foreach (var column in columns)
                {
                    dt.Columns.Add(column.Trim());
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Trim().Split(",");

                    if (fields.Length != columns.Length)
                    {
                        System.Console.WriteLine($"Line{i} has invlid data");
                        continue;
                    }

                    dt.Rows.Add(fields);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"Error: {e}");
                throw;
            }

            return dt;
        }
    }
}