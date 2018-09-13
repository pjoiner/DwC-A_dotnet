using DWC_A.Meta;
using System.Collections.Generic;

namespace DWC_A.Factories
{
    public interface IRowFactory
    {
        IRow CreateRow(IEnumerable<string> fields, IFieldMetaData fieldMetaData);
    }
}