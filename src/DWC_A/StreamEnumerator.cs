using Dwc.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DWC_A
{
    public class StreamEnumerator
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;
        private readonly IFileAttributes fileAttributes;
        private readonly IDictionary<string, int> fieldTypeIndex;
        private readonly Stream stream;

        public StreamEnumerator(Stream stream,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            ICollection<FieldType> fieldTypes,
            IFileAttributes fileAttributes)
        {
            this.stream = stream;
            this.fileAttributes = fileAttributes;
            this.rowFactory = rowFactory;
            this.fieldTypeIndex = fieldTypes.ToDictionary(k => k.Term, 
                v => Int32.TryParse(v.Index, out int value) ? value : 0);
            this.tokenizer = tokenizer;
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                Encoding encoding = Encoding.GetEncoding(fileAttributes.Encoding);
                using (TextReader reader = new StreamReader(stream, encoding, false, 1024, leaveOpen: true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return rowFactory.CreateRow(tokenizer.Split(line), fieldTypeIndex);
                    }
                }
            }
        }

        public IEnumerable<IRow> HeaderRows(int headerRowCount)
        {
            return Rows.Take(headerRowCount);
        }

        public IEnumerable<IRow> DataRows(int skipHeaderRows)
        {
            return Rows.Skip(skipHeaderRows);
        }
    }
}
