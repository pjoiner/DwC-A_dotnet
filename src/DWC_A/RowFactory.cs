using Dwc.Text;
using DWC_A;
using System.Collections.Generic;

namespace DWC_A
{
    public class RowFactory : IRowFactory
    {
        private ICollection<FieldType> fieldTypes;

        public RowFactory(ICollection<FieldType> fieldTypes)
        {
            this.fieldTypes = fieldTypes;
        }

        public IRow CreateRow(IEnumerable<string> fields)
        {
            return new Row(fields, fieldTypes);
        }
    }
}
