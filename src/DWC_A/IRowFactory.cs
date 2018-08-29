using System.Collections.Generic;

namespace DWC_A
{
    public interface IRowFactory
    {
        IRow CreateRow(IEnumerable<string> fields);
    }
}