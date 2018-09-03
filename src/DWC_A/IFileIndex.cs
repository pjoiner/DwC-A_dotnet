using System.Collections.Generic;

namespace DWC_A
{
    public interface IFileIndex
    {
        IEnumerable<long> OffsetsAt(string index);
    }
}