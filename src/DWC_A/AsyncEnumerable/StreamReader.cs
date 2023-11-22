using DwC_A.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DwC_A
{
#if !NETSTANDARD2_0
    internal partial class StreamReader
    {
        public async IAsyncEnumerable<IRow> ReadRowsAsync(Stream stream,
            [EnumeratorCancellation] CancellationToken ct = default)
        {
            using (var reader = new System.IO.StreamReader(stream, fileMetaData.Encoding))
            {
                string line;
                while ((line = await reader.ReadRowAsync(fileMetaData).ConfigureAwait(false)) != null)
                {
                    ct.ThrowIfCancellationRequested();
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                }
            }
        }
    }
#endif
}
