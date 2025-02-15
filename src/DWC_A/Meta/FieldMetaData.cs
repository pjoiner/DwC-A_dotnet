﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A.Meta
{
    internal class FieldMetaData : IFieldMetaData
    {
        private const string idFieldName = "id";
        private readonly IDictionary<string, FieldType> fieldIndexDictionary;
        private readonly FieldType[] fieldTypes;
        private readonly IEnumerable<FieldType> enumerableFields;

        public FieldMetaData(IdFieldType idFieldType, ICollection<FieldType> fieldTypes)
        {
            if (idFieldType != null && idFieldType.IndexSpecified
                && fieldTypes.Where(n => n.IndexSpecified)
                             .All(n => n.Index != idFieldType.Index))
            {
                var allFields = fieldTypes
                    .Append(new FieldType
                    {
                        Index = idFieldType.Index,
                        IndexSpecified = true,
                        Term = idFieldName
                    });
                this.fieldTypes = allFields 
                    .Where(n => n.IndexSpecified)
                    .OrderBy(n => n.Index)
                    .ToArray();
                this.fieldIndexDictionary = allFields
                    .ToDictionary(k => k.Term);
            }
            else
            {
                this.fieldTypes = fieldTypes
                    .Where(n => n.IndexSpecified)
                    .OrderBy(n => n.Index)
                    .ToArray();
                this.fieldIndexDictionary = fieldTypes
                    .ToDictionary(k => k.Term);
            }
            enumerableFields = this.fieldTypes.Union(fieldTypes.Where(n => !n.IndexSpecified));
        }

        public int IndexOf(string term)
        {
            if(fieldIndexDictionary.ContainsKey(term) && 
                fieldIndexDictionary[term].IndexSpecified )
            {
                return fieldIndexDictionary[term].Index;
            }
            return -1;
        }

        public IEnumerator<FieldType> GetEnumerator()
        {
            return enumerableFields.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerableFields.GetEnumerator();
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
                return fieldIndexDictionary[term];
            }
        }

        public bool TryGetFieldType(string term, out FieldType value)
        {
            if(fieldIndexDictionary.ContainsKey(term))
            {
                value = fieldIndexDictionary[term];
                return true;
            }
            value = null;
            return false;
        }

        public int Length => enumerableFields.Count();

        public int IndexedLength => fieldTypes.Length;

        public IEnumerable<FieldType> Indexed => fieldTypes;
    }
}
