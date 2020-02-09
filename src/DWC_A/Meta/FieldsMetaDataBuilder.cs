using System;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A.Meta
{
    public class FieldsMetaDataBuilder
    {
        private IList<FieldType> fieldsMetaData = new List<FieldType>();
        private bool automaticallyIndex = false;
        private int index = 0;

        private FieldsMetaDataBuilder()
        {
        }

        public static FieldsMetaDataBuilder Fields()
        {
            return new FieldsMetaDataBuilder();
        }

        public FieldsMetaDataBuilder AutomaticallyIndex()
        {
            automaticallyIndex = true;
            return this;
        }

        public FieldsMetaDataBuilder AddField( Func<FieldMetaDataBuilder, FieldMetaDataBuilder> fieldMetaData )
        {
            var fieldMetaDataBuilder = FieldMetaDataBuilder.Field();
            if(automaticallyIndex)
            {
                fieldMetaDataBuilder.Index(index++);
            }
            fieldsMetaData.Add(fieldMetaData(fieldMetaDataBuilder)
                .Build());
            return this;
        }

        public FieldType[] Build()
        {
            return fieldsMetaData.ToArray();
        }
    }
}
