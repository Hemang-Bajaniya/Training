public class DataDifference
{
    public int RowIndex { get; set; }
    public string ColumnName { get; set; }
    public object? OldValue { get; set; }
    public object? NewValue { get; set; }

    public override string ToString()
    {
        return $"Row {RowIndex}, Column '{ColumnName}': {OldValue} → {NewValue}";
    }
}
