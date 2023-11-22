using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DwC_A
{
#if !NETSTANDARD2_0
    internal partial class FileReader
    {
        public async IAsyncEnumerable<IRow> GetRowsAsync([EnumeratorCancellation] CancellationToken ct = default)
        {
            using (var stream = new FileStream(FileName,
                FileMode.Open, FileAccess.Read, FileShare.Read, config.BufferSize, true))
            {
                await foreach (var row in streamReader.ReadRowsAsync(stream, ct).ConfigureAwait(false))
                {
                    yield return row;
                }
            }
        }

        public async IAsyncEnumerable<IRow> GetHeaderRowsAsync([EnumeratorCancellation] CancellationToken ct = default)
        {
            int count = 0;
            await foreach (var row in GetRowsAsync(ct).ConfigureAwait(false))
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

        public async IAsyncEnumerable<IRow> GetDataRowsAsync([EnumeratorCancellation] CancellationToken ct = default)
        {
            int count = 0;
            await foreach (var row in GetRowsAsync(ct).ConfigureAwait(false))
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
