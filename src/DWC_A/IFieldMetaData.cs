using System.Collections.Generic;
using Dwc.Text;

namespace DWC_A
{
    public interface IFieldMetaData
    {
        IEnumerator<FieldType> GetEnumerator();
        int IndexOf(string term);
    }
}