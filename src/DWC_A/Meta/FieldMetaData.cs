using Dwc.Text;
using DWC_A.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DWC_A.Meta
{
    public class FieldMetaData : IEnumerable<FieldType>, IFieldMetaData
    {
        private const string idFieldName = "id"; 
        private readonly IdFieldType idFieldType;
        private readonly IDictionary<string, int> fieldIndexDictionary;
        private readonly IEnumerable<FieldType> fieldTypes;

        public FieldMetaData(IdFieldType idFieldType, ICollection<FieldType> fieldTypes)
        {
            this.idFieldType = idFieldType;
            if( idFieldType != null )
            {
                this.fieldTypes = fieldTypes.Append(new FieldType { Index = idFieldType.Index, Term = idFieldName })
                    .OrderBy(n => Convert.ToInt32(n.Index));
            }
            else
            {
                this.fieldTypes = fieldTypes
                    .OrderBy(n => Convert.ToInt32(n.Index));
            }
            this.fieldIndexDictionary = this.fieldTypes.ToDictionary(k => k.Term, v => Convert.ToInt32(v.Index));
        }

        public int IndexOf(string term)
        {
            if(!fieldIndexDictionary.ContainsKey(term))
            {
                throw new TermNotFoundException(term);
            }
            return fieldIndexDictionary[term];
        }

        public IEnumerator<FieldType> GetEnumerator()
        {
            return fieldTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return fieldTypes.GetEnumerator();
        }

        public FieldType this[int index]
        {
            get
            {
                return fieldTypes.ElementAt(index);
            }
        }

        public FieldType this[string term]
        {
            get
            {
                return fieldTypes.ElementAt(IndexOf(term));
            }
        }
    }
}
