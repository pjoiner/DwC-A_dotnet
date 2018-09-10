using DWC_A.Meta;
using System.Collections.Generic;

namespace DWC_A.Factories
{
    internal class RowFactory : IRowFactory
    {
        public IRow CreateRow(IEnumerable<string> fields, IFileMetaData fileMetaData)
        {
            return new Row(fields, fileMetaData);
        }
    }
}
