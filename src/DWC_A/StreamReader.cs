using DwC_A.Factories;
using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;

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
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                    CurrentOffset += fileMetaData.Encoding.GetByteCount(line) + fileMetaData.LineTerminatorLength;
                }
            }
        }

        public IEnumerable<IRow> ReadRowsAtOffset(Stream stream, IFileIndex index, string indexValue)
        {
            using (var reader = new System.IO.StreamReader(stream, fileMetaData.Encoding))
            {
                foreach (var offset in index.OffsetsAt(indexValue))
                {
                    reader.BaseStream.Seek(offset, 0);
                    CurrentOffset = offset;
                    reader.DiscardBufferedData();
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        throw new EndOfStreamException("Attempt to read offset beyond the end of file");
                    }
                    CurrentOffset += fileMetaData.Encoding.GetByteCount(line) + fileMetaData.LineTerminatorLength;
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                }
            }
        }

        public long CurrentOffset { get; private set; } = 0;
    }
}
