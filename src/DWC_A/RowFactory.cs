using System.Collections.Generic;

namespace DWC_A
{
    public class RowFactory : IRowFactory
    {
        public IRow CreateRow(IEnumerable<string> fields, IDictionary<string, int> fieldTypeIndex)
        {
            return new Row(fields, fieldTypeIndex);
        }
    }
}
