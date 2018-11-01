using DwC_A.Factories;
using DwC_A.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DwC_A
{
    internal class FileReader : IFileReader
    {
        private readonly StreamReader streamReader;
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
            streamReader = new StreamReader(rowFactory, tokenizer, fileMetaData);
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
                using (var stream = new FileStream(FileName, FileMode.Open))
                {
                    foreach(var row in streamReader.ReadRows(stream))
                    {
                        yield return row;
                    }
                }
            }
        }

        public IEnumerable<IRow> HeaderRows
        {
            get
            {
                return Rows.Take(FileMetaData.HeaderRowCount);
            }
        }

        public IEnumerable<IRow> DataRows
        {
            get
            {
                return Rows.Skip(FileMetaData.HeaderRowCount);
            }
        }

        public IFileIndex CreateIndexOn(string term, Action<int> progress = null)
        {
            var indexList = new List<KeyValuePair<string, long>>();
            using (var stream = new FileStream(FileName, FileMode.Open))
            {
                long blockSize = stream.Seek(0, SeekOrigin.End) / 100;
                stream.Seek(0, SeekOrigin.Begin);
                int percentComplete = 0;
                foreach (var row in streamReader.ReadRows(stream))
                {
                    indexList.Add(new KeyValuePair<string, long>(row[term], streamReader.CurrentOffset));
                    if (progress != null && (int)(streamReader.CurrentOffset / blockSize) > percentComplete)
                    {
                        percentComplete = (int)(streamReader.CurrentOffset / blockSize);
                        progress(percentComplete);
                    }
                }
            }
            progress?.Invoke(100);
            var index = indexFactory.CreateFileIndex(indexList);
            return index;
        }

        public IEnumerable<IRow> ReadRowsAtIndex(IFileIndex index, string indexValue)
        {
            using (var stream = new FileStream(FileName, FileMode.Open))
            {
                foreach(var row in streamReader.ReadRowsAtOffset(stream, index, indexValue))
                {
                    yield return row;
                }
            }
        }

        public string FileName { get; }

        public IFileMetaData FileMetaData { get; }

    }
}
