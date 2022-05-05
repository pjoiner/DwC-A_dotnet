namespace DwC_A.Meta
{
    /// <summary>
    /// Builds field metadata definitions
    /// </summary>
    public class FieldMetaDataBuilder
    {
        private int? index;
        private string term;
        private string defaultValue;
        private string vocabulary;
        private string delimiter;

        private FieldMetaDataBuilder(string term)
        {
            this.term = term;
        }

        /// <summary>
        /// Entry point for building field metatdata
        /// </summary>
        /// <param name="term">Term or column name.  Use Terms class to add well known terms</param>
        /// <returns>this</returns>
        public static FieldMetaDataBuilder Field(string term = null)
        {
            return new FieldMetaDataBuilder(term);
        }

        /// <summary>
        /// Numeric column number of field
        /// </summary>
        /// <param name="index">Zero based column number</param>
        /// <returns>this</returns>
        public FieldMetaDataBuilder Index(int index)
        {
            this.index = index;
            return this;
        }

        /// <summary>
        /// Sets the term for the field definition
        /// </summary>
        /// <param name="term">Use Terms class to lookup well known terms</param>
        /// <returns>this</returns>
        public FieldMetaDataBuilder Term(string term)
        {
            this.term = term;
            return this;
        }

        /// <summary>
        /// Sets the default value for the field
        /// </summary>
        /// <param name="defaultValue">A default value to be returned if no value is provided</param>
        /// <returns>this</returns>
        public FieldMetaDataBuilder Default(string defaultValue)
        {
            this.defaultValue = defaultValue;
            return this;
        }

        /// <summary>
        /// Sets the vocabulary for the field
        /// </summary>
        /// <param name="vocabulary">MUST be a Unified Resource Identifier (URI) for a vocabulary that the source values for this are based on.</param>
        /// <returns>this</returns>
        public FieldMetaDataBuilder Vocabulary(string vocabulary)
        {
            this.vocabulary = vocabulary;
            return this;
        }

        /// <summary>
        /// Sets the delimiter for the field if a list of values is stored in the field
        /// </summary>
        /// <param name="delimiter">Delimiter string</param>
        /// <returns>this</returns>
        public FieldMetaDataBuilder Delimiter(string delimiter)
        {
            this.delimiter = delimiter;
            return this;
        }

        /// <summary>
        /// Builds field metadata
        /// </summary>
        /// <returns>Field metadata</returns>
        public FieldType Build()
        {
            return new FieldType()
            {
                Index = index.GetValueOrDefault(),
                IndexSpecified = index.HasValue,
                Term = term,
                DelimitedBy = delimiter,
                Default = defaultValue,
                Vocabulary = vocabulary
            };
        }
    }
}
