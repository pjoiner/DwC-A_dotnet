using System.Collections.Generic;

namespace DwC_A
{
    public interface IFileIndex
    {
        IEnumerable<long> OffsetsAt(string index);
    }
}