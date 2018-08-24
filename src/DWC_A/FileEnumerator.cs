using System.Collections.Generic;
using System.IO;

namespace DWC_A
{
    public class FileEnumerator : IFileEnumerator
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;

        public FileEnumerator(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer )
        {
            this.FileName = fileName;
            this.rowFactory = rowFactory;
            this.tokenizer = tokenizer;
        }

        public string FileName { get; }

        public IEnumerable<IRow> Rows
        {
            get
            {
                using (Stream stream = new FileStream(FileName, FileMode.Open))
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
        }

    }
}
