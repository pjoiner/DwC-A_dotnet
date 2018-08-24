using Dwc.Text;
using DWC_A.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DWC_A
{
    public class Row : IRow
    {
        private readonly IEnumerable<string> fields;
        private readonly ICollection<FieldType> fieldTypes;

        public Row(IEnumerable<string> fields, ICollection<FieldType> fieldTypes)
        {
            this.fields = fields;
            this.fieldTypes = fieldTypes;
        }

        public IEnumerable<string> Fields
        {
            get
            {
                return fields;
            }
        }

        public string this[string term]
        {
            get
            {
                var field = fieldTypes.FirstOrDefault(n => n.Term == term);
                if(field == null)
                {
                    throw new TermNotFoundException(term);
                }
                return this[Convert.ToInt32(field.Index)];
            }
        }

        public string this[int index]
        {
            get
            {
                return fields.ElementAt(index);
            }
        }

    }

}
