﻿using DwC_A.Meta;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    internal class Row : IRow
    {
        private readonly string[] fields;

        public Row(IEnumerable<string> fields, IFieldMetaData fieldMetaData)
        {
            this.fields = fields.ToArray();
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
                var index = FieldMetaData.IndexOf(term);
                return this[index];
            }
        }

        public string this[int index]
        {
            get
            {
                return fields[index];
            }
        }
    }

}
