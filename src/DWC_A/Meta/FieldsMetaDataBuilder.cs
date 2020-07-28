using System;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A.Meta
{
    /// <summary>
    /// Creates a collection of field metadata
    /// </summary>
    public class FieldsMetaDataBuilder
    {
        private IList<FieldType> fieldsMetaData = new List<FieldType>();
        private bool automaticallyIndex = false;
        private int index = 0;

        private FieldsMetaDataBuilder()
        {
        }

        /// <summary>
        /// Entry point for building a collection of field metadata
        /// </summary>
        /// <returns>this</returns>
        public static FieldsMetaDataBuilder Fields()
        {
            return new FieldsMetaDataBuilder();
        }

        /// <summary>
        /// Use this method to automatically index fields in the order they are defined
        /// </summary>
        /// <returns>this</returns>
        public FieldsMetaDataBuilder AutomaticallyIndex()
        {
            automaticallyIndex = true;
            return this;
        }

        /// <summary>
        /// Add field metadata definition.  Call this for each field to define.
        /// </summary>
        /// <param name="fieldMetaData">This method will create a FieldMetaDataBuilder and pass it to a lamba defined to fill in the field metadat</param>
        /// <returns>this</returns>
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

        /// <summary>
        /// Builds the field metadata collection
        /// </summary>
        /// <returns>Field metadata collection</returns>
        public FieldType[] Build()
        {
            return fieldsMetaData.ToArray();
        }
    }
}
