using DwC_A.Meta;
using System.Collections.Generic;

namespace DwC_A
{
    /// <summary>
    /// Collection of fields
    /// </summary>
    public interface IRow
    {
        /// <summary>
        /// List of meta data properties for the fields in this row
        /// </summary>
        IFieldMetaData FieldMetaData { get; }
        /// <summary>
        /// An enumerable collection of field values
        /// </summary>
        IEnumerable<string> Fields { get; }
        /// <summary>
        /// Returns field value for a specified term
        /// </summary>
        /// <param name="term">Darwin Core Term</param>
        /// <returns>Field value</returns>
        string this[string term] { get; }
        /// <summary>
        /// Returns field value for a specified field index
        /// </summary>
        /// <param name="index">Index into collection of fields</param>
        /// <returns>Field value</returns>
        string this[int index] { get; }
        /// <summary>
        /// Attempts to return a value for a term or the default value if it has been defined in metadata
        /// </summary>
        /// <param name="term">Term</param>
        /// <param name="value">String value of field</param>
        /// <returns>True if found or default value returned.  False if not found.</returns>
        bool TryGetField(string term, out string value);
    }
}
