using System.Collections.Generic;
using System.Text;
using Dwc.Text;

namespace DWC_A
{
    public interface IFileMetaData
    {
        IFileAttributes Attributes { get; }
        string DateFormat { get; }
        Encoding Encoding { get; }
        IFieldMetaData Fields { get; }
        string FieldsEnclosedBy { get; }
        string FieldsTerminatedBy { get; }
        ICollection<FieldType> FieldTypes { get; }
        string FileName { get; }
        int HeaderRowCount { get; }
        IdFieldType Id { get; }
        string LinesTerminatedBy { get; }
        int LineTerminatorLength { get; }
        string RowType { get; }
    }
}