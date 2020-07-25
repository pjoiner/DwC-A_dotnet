using System.Text;

namespace DwC_A.Meta
{
    public class CoreFileMetaDataBuilder
    {
        private CoreFileType file;

        protected CoreFileMetaDataBuilder(string fileName)
        {
            file = new CoreFileType();
            file.Files.Add(fileName);
        }

        public static CoreFileMetaDataBuilder File(string fileName)
        {
            return new CoreFileMetaDataBuilder(fileName);
        }

        public CoreFileMetaDataBuilder LinesTerminatedBy(string lineTerminator)
        {
            file.FieldsTerminatedBy = lineTerminator;
            return this;
        }

        public CoreFileMetaDataBuilder FieldsTerminatedBy(string fieldTerminator)
        {
            file.FieldsTerminatedBy = fieldTerminator;
            return this;
        }

        public CoreFileMetaDataBuilder FieldsEnclosedBy(string fieldsEnclosedBy)
        {
            file.FieldsEnclosedBy = fieldsEnclosedBy;
            return this;
        }

        public CoreFileMetaDataBuilder IgnoreHeaderLines(int ignoreHeaderLines)
        {
            file.IgnoreHeaderLines = ignoreHeaderLines;
            return this;
        }

        public CoreFileMetaDataBuilder RowType(string rowType)
        {
            file.RowType = rowType;
            return this;
        }

        public CoreFileMetaDataBuilder Encoding(Encoding encoding)
        {
            file.Encoding = encoding.WebName.ToUpper();
            return this;
        }

        public CoreFileMetaDataBuilder DateFormat(string dateFormat)
        {
            file.DateFormat = dateFormat;
            return this;
        }

        public CoreFileMetaDataBuilder Index(int index)
        {
            file.Id = new IdFieldType()
            {
                Index = index,
                IndexSpecified = true
            };
            return this;
        }

        public CoreFileMetaDataBuilder AddField(FieldType field)
        {
            file.Field.Add(field);
            return this;
        }

        public CoreFileMetaDataBuilder AddField(FieldMetaDataBuilder field)
        {
            AddField(field.Build());
            return this;
        }

        public CoreFileMetaDataBuilder AddFields(FieldsMetaDataBuilder fields)
        {
            foreach(var field in fields.Build()) 
            {
                AddField(field);
            }
            return this;
        }

        public CoreFileType Build()
        {
            return file;
        }
    }
}
