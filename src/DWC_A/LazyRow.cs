using DwC_A.Exceptions;
using DwC_A.Meta;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    internal class LazyRow : IRow
    {
        private readonly IEnumerable<string> fields;

        public LazyRow(IEnumerable<string> fields, IFieldMetaData fieldMetaData)
        {
            this.fields = fields;
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
                if (TryGetField(term, out string value))
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
                return fields.ElementAt(index);
            }
        }

        public bool TryGetField(string term, out string value)
        {
            if (FieldMetaData.TryGetFieldType(term, out FieldType fieldType) &&
                fieldType.Index < FieldMetaData.Length)
            {
                value = fieldType.IndexSpecified && !string.IsNullOrEmpty(this[fieldType.Index]) ? this[fieldType.Index] : fieldType.Default;
                return true;
            }
            value = null;
            return false;
        }

    }
}
