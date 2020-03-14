using DwC_A.Extensions;
using DwC_A.Factories;
using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DwC_A
{
    internal class StreamReader 
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;
        private readonly IFileMetaData fileMetaData;

        public StreamReader(
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData;
            this.rowFactory = rowFactory;
            this.tokenizer = tokenizer;
        }

        public IEnumerable<IRow> ReadRows(Stream stream)
        {
            using(var reader = new System.IO.StreamReader(stream, fileMetaData.Encoding))
            {
                StringBuilder line = new StringBuilder();
                while(reader.ReadRow(fileMetaData, ref line))
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line.Flush()), fileMetaData.Fields);
                }
            }
        }

        public async IAsyncEnumerable<IRow> ReadRowsAsync(Stream stream)
        {
            using(var reader = new System.IO.StreamReader(stream, fileMetaData.Encoding))
            {
                string line;
                while((line = await reader.ReadLineAsync()) != null)
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                }
            }
        }
    }
}
