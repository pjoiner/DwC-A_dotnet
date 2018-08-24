using System.Collections.Generic;

namespace DWC_A
{
    public interface IRow
    {
        IEnumerable<string> Fields { get; }
        string this[string term] { get; }
        string this[int index] { get; }
    }


}
