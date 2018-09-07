using System.Collections.Generic;

namespace DWC_A.Factories
{
    public interface IIndexFactory
    {
        IFileIndex CreateFileIndex(IList<KeyValuePair<string, long>> offsetList);
    }
}