using System.Collections.Generic;

namespace DWC_A
{
    public class RowFactory : IRowFactory
    {
        public IRow CreateRow(IEnumerable<string> fields, IFileMetaData fileMetaData)
        {
            return new Row(fields, fileMetaData);
        }
    }
}
