using DwC_A.Extensions;
using System.Collections.Generic;
using System.IO;

namespace DwC_A
{
#if !NETSTANDARD2_0
    internal partial class StreamReader
    {
        public async IAsyncEnumerable<IRow> ReadRowsAsync(Stream stream)
        {
            using (var reader = new System.IO.StreamReader(stream, fileMetaData.Encoding))
            {
                string line;
                while ((line = await reader.ReadRowAsync(fileMetaData)) != null)
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                }
            }
        }
    }
#endif
}
