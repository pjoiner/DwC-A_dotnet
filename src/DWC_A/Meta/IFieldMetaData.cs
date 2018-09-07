using System.Collections.Generic;
using Dwc.Text;

namespace DWC_A.Meta
{
    public interface IFieldMetaData
    {
        IEnumerator<FieldType> GetEnumerator();
        int IndexOf(string term);
        FieldType this[int index] { get; }
        FieldType this[string term] { get; }
    }
}