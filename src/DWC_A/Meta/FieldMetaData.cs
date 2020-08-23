using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A.Meta
{
    internal class FieldMetaData : IFieldMetaData
    {
        private const string idFieldName = "id";
        private readonly IDictionary<string, int> fieldIndexDictionary;
        private readonly FieldType[] fieldTypes;

        public FieldMetaData(IdFieldType idFieldType, ICollection<FieldType> fieldTypes)
        {
            if (idFieldType != null && idFieldType.IndexSpecified
                && fieldTypes.All(n => n.Index != idFieldType.Index))
            {
                this.fieldTypes = fieldTypes.Append(new FieldType { Index = idFieldType.Index, Term = idFieldName })
                    .OrderBy(n => n.Index)
                    .ToArray();
            }
            else
            {
                this.fieldTypes = fieldTypes
                    .OrderBy(n => n.Index)
                    .ToArray();
            }
            this.fieldIndexDictionary = this.fieldTypes.ToDictionary(k => k.Term, v => v.Index);
        }

        public int IndexOf(string term)
        {
            if (!fieldIndexDictionary.ContainsKey(term))
            {
                return -1;
            }
            return fieldIndexDictionary[term];
        }

        public IEnumerator<FieldType> GetEnumerator()
        {
            return fieldTypes.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return fieldTypes.GetEnumerator();
        }

        public FieldType this[int index]
        {
            get
            {
                return fieldTypes[index];
            }
        }

        public FieldType this[string term]
        {
            get
            {
                return fieldTypes[IndexOf(term)];
            }
        }
    }
}
