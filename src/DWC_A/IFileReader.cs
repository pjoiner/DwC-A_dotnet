using DwC_A.Meta;
using System;
using System.Collections.Generic;

namespace DwC_A
{
    /// <summary>
    /// Reads a file
    /// </summary>
    public interface IFileReader : IDisposable
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
        IEnumerable<IRow> DataRows { get; }
        /// <summary>
        /// Enumerable collection of header row objects
        /// </summary>
        IEnumerable<IRow> HeaderRows { get; }
        /// <summary>
        /// Enumerable collection of all row objects including headers and data
        /// </summary>
        IEnumerable<IRow> Rows { get; }
        /// <summary>
        /// Creates an index on the file for the term
        /// </summary>
        /// <param name="term">Darwin Core Term</param>
        /// <param name="progress">Optional delegate to display progress</param>
        /// <returns>Interface to an index collection</returns>
        IFileIndex CreateIndexOn(string term, Action<int> progress = null);
        /// <summary>
        /// Returns an enumerable collection of row objects for a specified index
        /// </summary>
        /// <param name="index">Index collection created with <seealso cref="CreateIndexOn(string, Action{int})"/></param>
        /// <param name="indexValue"></param>
        /// <returns>An enumerable collection of row objects</returns>
        IEnumerable<IRow> ReadRowsAtIndex(IFileIndex index, string indexValue);
    }
}