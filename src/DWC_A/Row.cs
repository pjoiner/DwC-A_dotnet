using DWC_A.Meta;
using System.Collections.Generic;
using System.Linq;

namespace DWC_A
{
    public class Row : IRow
    {
        private readonly IFileMetaData fileMetaData;

        public Row(IEnumerable<string> fields, IFileMetaData fileMetaData)
        {
            this.Fields = fields;
            this.fileMetaData = fileMetaData;
        }

        public IEnumerable<string> Fields { get; }

        public string this[string term]
        {
            get
            {
                var index = fileMetaData.Fields.IndexOf(term);
                return this[index];
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
