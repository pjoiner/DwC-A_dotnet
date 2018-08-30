using DWC_A.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace DWC_A
{
    public class Row : IRow
    {
        private readonly IDictionary<string, int> fieldTypeIndex;

        public Row(IEnumerable<string> fields, IDictionary<string, int> fieldTypeIndex)
        {
            this.Fields = fields;
            this.fieldTypeIndex = fieldTypeIndex;
        }

        public IEnumerable<string> Fields { get; }

        public string this[string term]
        {
            get
            {
                if(!fieldTypeIndex.ContainsKey(term))
                {
                    throw new TermNotFoundException(term);
                }
                return this[fieldTypeIndex[term]];
            }
        }

        public string this[int index]
        {
            get
            {
                return Fields.ElementAt(index);
            }
        }

    }

}
