using Dwc.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace DWC_A
{
    public class FileReader : IDisposable, IFileReader
    {
        private readonly StreamEnumerator streamEnumerator;
        private Stream stream;
        private readonly IFileAttributes fileAttributes;
        private bool disposed = false;

        public FileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileAttributes fileAttributes,
            ICollection<FieldType> fieldTypes)
        {
            this.FileName = fileName;
            this.fileAttributes = fileAttributes;
            this.FieldTypes = fieldTypes;
            stream = new FileStream(fileName, FileMode.Open);
            streamEnumerator = new StreamEnumerator(stream, rowFactory, tokenizer, fieldTypes);
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                stream.Seek(0, 0);
                return streamEnumerator.Rows;
            }
        }

        public IEnumerable<IRow> HeaderRows
        {
            get
            {
                stream.Seek(0, 0);
                return streamEnumerator.HeaderRows(HeaderRowCount);
            }
        }

        public IEnumerable<IRow> DataRows
        {
            get
            {
                stream.Seek(0, 0);
                return streamEnumerator.DataRows(HeaderRowCount);
            }
        }

        private int HeaderRowCount
        {
            get
            {
                if (!Int32.TryParse(fileAttributes.IgnoreHeaderLines, out int headerRowCount))
                {
                    headerRowCount = 0;
                }

                return headerRowCount;
            }
        }

        public string FileName { get; private set; }

        public ICollection<FieldType> FieldTypes { get; private set; }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                {
                    // free managed resources
                    if (stream != null)
                    {
                        stream.Dispose();
                        stream = null;
                    }
                }
            }
            disposed = true;
        }

        ~FileReader()
        {
            Dispose(false);
        }
        #endregion
    }
}
