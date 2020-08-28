using DwC_A.Exceptions;
using DwC_A.Extensions;
using DwC_A.Meta;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    internal class Row : IRow
    {
        private readonly string[] fields;

        public Row(IEnumerable<string> fields, IFieldMetaData fieldMetaData)
        {
            this.fields = fields.ToArray(fieldMetaData.Length);
            this.FieldMetaData = fieldMetaData;
        }

        public IEnumerable<string> Fields
        {
            get
            {
                return fields;
            }
        }

        public IFieldMetaData FieldMetaData { get; }

        public string this[string term]
        {
            get
            {
                if(TryGetField(term, out string value))
                {
                    return value;
                }
                throw new TermNotFoundException(term);
            }
        }

        public string this[int index]
        {
            get
            {
                return fields[index];
            }
        }

        public bool TryGetField(string term, out string value)
        {
            if (FieldMetaData.TryGetFieldType(term, out FieldType fieldType) &&
                fieldType.Index < fields.Length)
            {
                value = fieldType.IndexSpecified && !string.IsNullOrEmpty(this[fieldType.Index]) ? this[fieldType.Index] : fieldType.Default;
                return true;
            }
            value = null;
            return false;
        }

    }

}
