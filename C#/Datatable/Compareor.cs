using System.Data;

public class DataTableComparer : IDataTableComparer
{
    public List<DataDifference> Compare(DataTable oldTable, DataTable newTable)
    {
        var diffs = new List<DataDifference>();

        if (oldTable.Rows.Count != newTable.Rows.Count || 
            oldTable.Columns.Count != newTable.Columns.Count)
        {
            throw new InvalidOperationException("Tables must have the same structure.");
        }

        for (int i = 0; i < oldTable.Rows.Count; i++)
        {
            for (int j = 0; j < oldTable.Columns.Count; j++)
            {
                var column = oldTable.Columns[j].ColumnName;
                var oldVal = oldTable.Rows[i][j];
                var newVal = newTable.Rows[i][j];

                // Compare by value (handling DBNull and type safety)
                if (!object.Equals(oldVal, newVal))
                {
                    diffs.Add(new DataDifference
                    {
                        RowIndex = i,
                        ColumnName = column,
                        OldValue = oldVal == DBNull.Value ? null : oldVal,
                        NewValue = newVal == DBNull.Value ? null : newVal
                    });
                }
            }
        }

        return diffs;
    }
}
