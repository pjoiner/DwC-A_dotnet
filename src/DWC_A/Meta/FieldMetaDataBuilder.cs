namespace DwC_A.Meta
{
    /// <summary>
    /// Builds field metadata definitions
    /// </summary>
    public class FieldMetaDataBuilder
    {
        private int? index;
        private string term;

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
        /// Builds field metadata
        /// </summary>
        /// <returns>Field metadata</returns>
        public FieldType Build()
        {
            return new FieldType()
            {
                Index = index.GetValueOrDefault(),
                IndexSpecified = index.HasValue,
                Term = term
            };
        }
    }
}
