using System.Collections.Generic;

namespace DwC_A.Meta
{
    /// <summary>
    /// Collection of meta data for fields
    /// </summary>
    public interface IFieldMetaData: IEnumerable<FieldType>
    {
        /// <summary>
        /// Retrieves index for a term
        /// </summary>
        /// <param name="term">Darwin Core Term</param>
        /// <returns>Index of column containing the term.  If the term is not found then returns -1.</returns>
        int IndexOf(string term);
        /// <summary>
        /// Retrieves field at index
        /// </summary>
        /// <param name="index">Index or column number</param>
        /// <returns>String representation of field</returns>
        FieldType this[int index] { get; }
        /// <summary>
        /// Retrieves field data for a term
        /// </summary>
        /// <param name="term">Darwin Core Term</param>
        /// <returns>String representation of field</returns>
        FieldType this[string term] { get; }
        /// <summary>
        /// Attempts to retrieve the field by term name
        /// </summary>
        /// <param name="term">Darwin Core Term</param>
        /// <param name="value">FieldType</param>
        /// <returns>Returns false if not found</returns>
        bool TryGetFieldType(string term, out FieldType value);
        /// <summary>
        /// Returns the number of fields (including defaults)
        /// </summary>
        int Length { get; }
        /// <summary>
        /// Returns the number of indexed fields
        /// </summary>
        int IndexedLength { get; }
        /// <summary>
        /// Returns a list of indexed fields in index order
        /// </summary>
        IEnumerable<FieldType> Indexed { get; }
    }
}