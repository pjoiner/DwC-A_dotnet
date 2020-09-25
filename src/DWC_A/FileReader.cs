using DwC_A.Config;
using DwC_A.Factories;
using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DwC_A
{
    internal partial class FileReader : IFileReaderAggregate
    {
        private readonly StreamReader streamReader;
        private readonly FileReaderConfiguration config;

        public FileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData,
            FileReaderConfiguration config)
        {
            this.config = config;
            this.FileName = fileName;
            this.FileMetaData = fileMetaData;
            FileReaderUtils.ValidateLineEnds(fileMetaData.LinesTerminatedBy);
            streamReader = new StreamReader(rowFactory, tokenizer, fileMetaData);
        }

        public IEnumerable<IRow> Rows
        {
            get
            {
                using (var stream = new FileStream(FileName, 
                    FileMode.Open, FileAccess.Read, FileShare.Read, config.BufferSize, false))
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
