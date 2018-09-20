using DwC_A.Factories;
using DwC_A.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DwC_A
{
    internal class StreamReader : IDisposable
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;
        private readonly IFileMetaData fileMetaData;
        private readonly Stream stream;
        private readonly System.IO.StreamReader reader;

        public StreamReader(Stream stream,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData)
        {
            this.stream = stream;
            this.fileMetaData = fileMetaData;
            this.rowFactory = rowFactory;
            this.tokenizer = tokenizer;
            reader = new System.IO.StreamReader(stream, fileMetaData.Encoding);
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                CurrentOffset = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                    CurrentOffset += fileMetaData.Encoding.GetByteCount(line) + fileMetaData.LineTerminatorLength;
                }
            }
        }

        public IRow ReadRowAtOffset(long offset)
        {
            CurrentOffset = offset;
            reader.DiscardBufferedData();
            string line = reader.ReadLine();
            if (line == null)
            {
                throw new EndOfStreamException("Attempt to read offset beyond the end of file");
            }
            CurrentOffset += fileMetaData.Encoding.GetByteCount(line) + fileMetaData.LineTerminatorLength;
            return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
        }

        public IEnumerable<IRow> HeaderRows(int headerRowCount)
        {
            return Rows.Take(headerRowCount);
        }

        public IEnumerable<IRow> DataRows(int skipHeaderRows)
        {
            return Rows.Skip(skipHeaderRows);
        }

        public long CurrentOffset { get; private set; } = 0;

        #region IDisposable
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // free managed resources
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
            disposed = true;
        }

        ~StreamReader()
        {
            Dispose(false);
        }
        #endregion

    }
}
