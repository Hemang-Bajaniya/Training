namespace LibraryCheckIn.IO
{
    /// <summary>
    /// Abstraction for writing reports to disk.
    /// </summary>
    public interface IReportWriter<T>
    {
        /// <summary>
        /// Writes the given items to the given file path.
        /// </summary>
        void Write(IEnumerable<T> items, string filePath);
    }
}
