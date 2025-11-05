public interface IDataTableComparer
{
    List<DataDifference> Compare(DataTable oldTable, DataTable newTable);
}
