using DwC_A.Meta;
using System.Collections.Generic;
using System.Threading;

namespace DwC_A
{
#if !NETSTANDARD2_0
    /// <summary>
    /// Reads a file
    /// </summary>
    public interface IAsyncFileReader
    {
        /// <summary>
        /// Fully qualified path to file
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Collection of metadata for file
        /// </summary>
        IFileMetaData FileMetaData { get; }
        /// <summary>
        /// Enumerable collection of data row objects
        /// </summary>
        IAsyncEnumerable<IRow> GetDataRowsAsync(CancellationToken ct = default);
        /// <summary>
        /// Enumerable collection of header row objects
        /// </summary>
        IAsyncEnumerable<IRow> GetHeaderRowsAsync(CancellationToken ct = default);
        /// <summary>
        /// Enumerable collection of all row objects including headers and data
        /// </summary>
        IAsyncEnumerable<IRow> GetRowsAsync(CancellationToken ct = default);
    }
#endif
}