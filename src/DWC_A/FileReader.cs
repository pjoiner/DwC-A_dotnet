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

        public FileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData)
        {
            this.FileName = fileName;
            this.FileMetaData = fileMetaData;
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

        public string FileName { get; }

        public IFileMetaData FileMetaData { get; }
    }
}
