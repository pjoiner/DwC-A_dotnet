using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DWC_A
{
    public class FileIndex : IFileIndex
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
