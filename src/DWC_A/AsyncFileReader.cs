using DwC_A.Factories;
using DwC_A.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DwC_A
{
    internal class AsyncFileReader : IAsyncFileReader
    {
        private readonly StreamReader streamReader;

        public AsyncFileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData)
        {
            this.FileName = fileName;
            this.FileMetaData = fileMetaData;
            FileReaderUtils.ValidateLineEnds(fileMetaData.LinesTerminatedBy);
            streamReader = new StreamReader(rowFactory, tokenizer, fileMetaData);
        }

        public async IAsyncEnumerable<IRow> GetRowsAsync()
        {
            using (var stream = new FileStream(FileName, FileMode.Open))
            {
                await foreach(var row in streamReader.ReadRowsAsync(stream))
                {
                    yield return row;
                }
            }
        }

        public async IAsyncEnumerable<IRow> GetHeaderRowsAsync()
        {
            int count = 0;
            await foreach(var row in GetRowsAsync())
            {
                if(count < FileMetaData.HeaderRowCount)
                {
                    yield return row;
                }
                else
                {
                    break;
                }
                count++;
            }
        }

        public async IAsyncEnumerable<IRow> GetDataRowsAsync()
        {
            int count = 0;
            await foreach(var row in GetRowsAsync())
            {
                if(count >= FileMetaData.HeaderRowCount)
                {
                    yield return row;
                }
                else
                {
                    count++;
                }
            }
        }

        public string FileName { get; }

        public IFileMetaData FileMetaData { get; }
    }
}
