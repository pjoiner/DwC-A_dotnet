using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DWC_A
{
    public class StreamEnumerator
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;
        private readonly Stream stream;

        public StreamEnumerator(Stream stream,
            IRowFactory rowFactory,
            ITokenizer tokenizer )
        {
            this.stream = stream;
            this.rowFactory = rowFactory;
            this.tokenizer = tokenizer;
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return rowFactory.CreateRow(tokenizer.Split(line));
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
