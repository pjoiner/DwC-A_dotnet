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
        private bool disposed = false;
        private readonly IFileAttributes fileAttributes;
        private readonly IIndexFactory indexFactory;

        public FileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileAttributes fileAttributes,
            ICollection<FieldType> fieldTypes,
            IIndexFactory indexFactory)
        {
            this.FileName = fileName;
            this.fileAttributes = fileAttributes;
            this.FieldTypes = fieldTypes;
            this.indexFactory = indexFactory;
            stream = new FileStream(fileName, FileMode.Open);
            streamEnumerator = new StreamEnumerator(stream, rowFactory, tokenizer, fieldTypes, fileAttributes);
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

        public IFileIndex CreateIndexOn(string term)
        {
            var indexList = new List<KeyValuePair<string, long>>();
            foreach(var row in DataRows)
            {
                indexList.Add(KeyValuePair.Create(row[term], streamEnumerator.CurrentOffset));
            }
            return indexFactory.CreateFileIndex(indexList);
        }

        public IEnumerable<IRow> ReadRowsAtIndex(IFileIndex index, string indexValue)
        {
            foreach(var offset in index.OffsetsAt(indexValue))
            {
                stream.Seek(offset, 0);
                yield return streamEnumerator.ReadRowAtOffset(offset);
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
                    streamEnumerator.Dispose();
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
