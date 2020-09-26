using System.Collections.Generic;
using System.IO;

namespace DwC_A
{
#if NETSTANDARD2_1
    internal partial class FileReader
    {
        public async IAsyncEnumerable<IRow> GetRowsAsync()
        {
            using (var stream = new FileStream(FileName,
                FileMode.Open, FileAccess.Read, FileShare.Read, config.BufferSize, true))
            {
                await foreach (var row in streamReader.ReadRowsAsync(stream))
                {
                    yield return row;
                }
            }
        }

        public async IAsyncEnumerable<IRow> GetHeaderRowsAsync()
        {
            int count = 0;
            await foreach (var row in GetRowsAsync())
            {
                if (count < FileMetaData.HeaderRowCount)
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
            await foreach (var row in GetRowsAsync())
            {
                if (count >= FileMetaData.HeaderRowCount)
                {
                    yield return row;
                }
                else
                {
                    count++;
                }
            }
        }
    }
#endif
}
