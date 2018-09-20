using System.Collections.Generic;

namespace DwC_A.Factories
{
    public interface IIndexFactory
    {
        IFileIndex CreateFileIndex(IList<KeyValuePair<string, long>> offsetList);
    }
}