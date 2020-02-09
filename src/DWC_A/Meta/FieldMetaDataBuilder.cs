namespace DwC_A.Meta
{
    public class FieldMetaDataBuilder
    {
        private int? index;
        private string term;

        private FieldMetaDataBuilder(string term)
        {
            this.term = term;
        }

        public static FieldMetaDataBuilder Field(string term = null)
        {
            return new FieldMetaDataBuilder(term);
        }

        public FieldMetaDataBuilder Index(int index)
        {
            this.index = index;
            return this;
        }

        public FieldMetaDataBuilder Term(string term)
        {
            this.term = term;
            return this;
        }

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
