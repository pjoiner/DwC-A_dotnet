using System.Collections.Generic;
using DWC_A;

namespace DWC_A
{
    public interface IRowFactory
    {
        IRow CreateRow(IEnumerable<string> fields);
    }
}