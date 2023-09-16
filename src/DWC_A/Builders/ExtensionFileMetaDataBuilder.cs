using System.Text;
using DwC_A.Meta;

namespace DwC_A.Builders
{
    public class ExtensionFileMetaDataBuilder
    {
        private ExtensionFileType file;

        protected ExtensionFileMetaDataBuilder(string fileName)
        {
            file = new ExtensionFileType();
            file.Files.Add(fileName);
        }

        public static ExtensionFileMetaDataBuilder File(string fileName)
        {
            return new ExtensionFileMetaDataBuilder(fileName);
        }

        public ExtensionFileMetaDataBuilder LinesTerminatedBy(string lineTerminator)
        {
            file.FieldsTerminatedBy = lineTerminator;
            return this;
        }

        public ExtensionFileMetaDataBuilder FieldsTerminatedBy(string fieldTerminator)
        {
            file.FieldsTerminatedBy = fieldTerminator;
            return this;
        }

        public ExtensionFileMetaDataBuilder FieldsEnclosedBy(string fieldsEnclosedBy)
        {
            file.FieldsEnclosedBy = fieldsEnclosedBy;
            return this;
        }

        public ExtensionFileMetaDataBuilder IgnoreHeaderLines(int ignoreHeaderLines)
        {
            file.IgnoreHeaderLines = ignoreHeaderLines;
            return this;
        }

        public ExtensionFileMetaDataBuilder RowType(string rowType)
        {
            file.RowType = rowType;
            return this;
        }

        public ExtensionFileMetaDataBuilder Encoding(Encoding encoding)
        {
            file.Encoding = encoding.WebName.ToUpper();
            return this;
        }

        public ExtensionFileMetaDataBuilder DateFormat(string dateFormat)
        {
            file.DateFormat = dateFormat;
            return this;
        }

        public ExtensionFileMetaDataBuilder CoreIndex(int index)
        {
            file.Coreid = new IdFieldType()
            {
                Index = index,
                IndexSpecified = true
            };
            return this;
        }

        public ExtensionFileMetaDataBuilder AddField(FieldType field)
        {
            file.Field.Add(field);
            return this;
        }

        public ExtensionFileMetaDataBuilder AddField(FieldMetaDataBuilder field)
        {
            AddField(field.Build());
            return this;
        }

        public ExtensionFileMetaDataBuilder AddFields(FieldsMetaDataBuilder fields)
        {
            foreach (var field in fields.Build())
            {
                AddField(field);
            }
            return this;
        }

        public ExtensionFileType Build()
        {
            return file;
        }

    }
}
