using System.Collections.Generic;

namespace DWC_A
{
    public interface IFileEnumerator
    {
        string FileName { get; }
        IEnumerable<IRow> Rows { get; }
    }
}