using System.Text;
using Dwc.Text;

namespace DWC_A.Meta
{
    public interface IFileMetaData
    {
        string DateFormat { get; }
        Encoding Encoding { get; }
        IFieldMetaData Fields { get; }
        string FieldsEnclosedBy { get; }
        string FieldsTerminatedBy { get; }
        string FileName { get; }
        int HeaderRowCount { get; }
        IdFieldType Id { get; }
        string LinesTerminatedBy { get; }
        int LineTerminatorLength { get; }
        string RowType { get; }
    }
}