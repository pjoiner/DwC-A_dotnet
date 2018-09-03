using System.Collections.Generic;

namespace DWC_A
{
    public interface IIndexFactory
    {
        IFileIndex CreateFileIndex(IList<KeyValuePair<string, long>> offsetList);
    }
}