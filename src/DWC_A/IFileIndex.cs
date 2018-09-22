using System.Collections.Generic;

namespace DwC_A
{
    /// <summary>
    /// Interface for file index objects
    /// </summary>
    public interface IFileIndex
    {
        /// <summary>
        /// An enumerable list of file offsets for a field value.
        /// </summary>
        /// <param name="index">Value of field to index on.</param>
        /// <returns>List of offsets for rows that contain the index value.</returns>
        IEnumerable<long> OffsetsAt(string index);
    }
}