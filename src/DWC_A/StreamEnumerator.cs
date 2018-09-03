using Dwc.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DWC_A
{
    public class StreamEnumerator : IDisposable
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;
        private readonly IFileAttributes fileAttributes;
        private readonly IDictionary<string, int> fieldTypeIndex;
        private readonly Stream stream;
        private readonly TextReader reader;
        private readonly Encoding encoding;
        private readonly long lineEndingSize;

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
            encoding = Encoding.GetEncoding(fileAttributes.Encoding);
            reader = new StreamReader(stream, encoding);
            lineEndingSize = encoding.GetByteCount(Regex.Unescape(fileAttributes.LinesTerminatedBy));
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                CurrentOffset = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fieldTypeIndex);
                    CurrentOffset += encoding.GetByteCount(line) + lineEndingSize;
                }
            }
        }

        public IRow ReadRowAtOffset(long offset)
        {
            CurrentOffset = offset;
            string line = reader.ReadLine();
            if (line == null)
            {
                throw new EndOfStreamException("Attempt to read offset beyond the end of file");
            }
            CurrentOffset += encoding.GetByteCount(line) + lineEndingSize;
            return rowFactory.CreateRow(tokenizer.Split(line), fieldTypeIndex);
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

        ~StreamEnumerator()
        {
            Dispose(false);
        }
        #endregion

    }
}
