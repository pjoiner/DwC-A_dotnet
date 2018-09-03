using System.Collections.Generic;

namespace DWC_A
{
    public class IndexFactory : IIndexFactory
    {
        public IFileIndex CreateFileIndex(IList<KeyValuePair<string, long>> offsetList)
        {
            return new FileIndex(offsetList);
        }
    }
}
