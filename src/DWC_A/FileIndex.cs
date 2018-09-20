using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    internal class FileIndex : IFileIndex
    {
        private readonly ILookup<string, long> lookup;

        public FileIndex(IEnumerable<KeyValuePair<string,long>> indexList)
        {
            lookup = indexList.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        }

        public IEnumerable<long> OffsetsAt(string index)
        {
            return lookup[index];
        }

    }
}
