using DwC_A.Factories;
using DwC_A.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DwC_A
{
    internal class FileReader : IDisposableFileReader
    {
        private readonly StreamReader streamReader;
        private Stream stream;
        private bool disposed = false;
        private readonly IIndexFactory indexFactory;

        public FileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData,
            IIndexFactory indexFactory)
        {
            this.FileName = fileName;
            this.FileMetaData = fileMetaData;
            this.indexFactory = indexFactory;
            ValidateLineEnds(fileMetaData.LinesTerminatedBy);
            stream = new FileStream(fileName, FileMode.Open);
            streamReader = new StreamReader(stream, rowFactory, tokenizer, fileMetaData);
        }

        private void ValidateLineEnds(string linesTerminatedBy)
        {
            if (new[] { "\n", "r", "\r\n" }.Contains(linesTerminatedBy) == false)
            {
                throw new NotSupportedException($"Only files terminated by '\n', '\r' or '\r\n' are supported.");
            }
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                stream.Seek(0, 0);
                return streamReader.Rows;
            }
        }

        public IEnumerable<IRow> HeaderRows
        {
            get
            {
                stream.Seek(0, 0);
                return streamReader.HeaderRows(FileMetaData.HeaderRowCount);
            }
        }

        public IEnumerable<IRow> DataRows
        {
            get
            {
                stream.Seek(0, 0);
                return streamReader.DataRows(FileMetaData.HeaderRowCount);
            }
        }

        public IFileIndex CreateIndexOn(string term, Action<int> progress = null)
        {
            var indexList = new List<KeyValuePair<string, long>>();
            long blockSize = stream.Seek(0, SeekOrigin.End) / 100;
            int percentComplete = 0;
            foreach (var row in DataRows)
            {
                indexList.Add(new KeyValuePair<string, long>(row[term], streamReader.CurrentOffset));
                if (progress != null && (int)(streamReader.CurrentOffset / blockSize) > percentComplete)
                {
                    percentComplete = (int)(streamReader.CurrentOffset / blockSize);
                    progress(percentComplete);
                }
            }
            progress?.Invoke(100);
            var index = indexFactory.CreateFileIndex(indexList);
            return index;
        }

        public IEnumerable<IRow> ReadRowsAtIndex(IFileIndex index, string indexValue)
        {
            foreach(var offset in index.OffsetsAt(indexValue))
            {
                stream.Seek(offset, 0);
                yield return streamReader.ReadRowAtOffset(offset);
            }
        }

        public string FileName { get; }

        public IFileMetaData FileMetaData { get; }

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
                    streamReader.Dispose();
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
