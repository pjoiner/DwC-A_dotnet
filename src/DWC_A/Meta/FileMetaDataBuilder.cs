using System.Text;

namespace DwC_A.Meta
{
    public class FileMetaDataBuilder
    {
        private FileType file;

        private FileMetaDataBuilder(string fileName)
        {
            file = new FileType();
            file.Files.Add(fileName);
        }

        public static FileMetaDataBuilder File(string fileName)
        {
            return new FileMetaDataBuilder(fileName);
        }

        public FileMetaDataBuilder LinesTerminatedBy(string lineTerminator)
        {
            file.FieldsTerminatedBy = lineTerminator;
            return this;
        }

        public FileMetaDataBuilder FieldsTerminatedBy(string fieldTerminator)
        {
            file.FieldsTerminatedBy = fieldTerminator;
            return this;
        }

        public FileMetaDataBuilder FieldsEnclosedBy(string fieldsEnclosedBy)
        {
            file.FieldsEnclosedBy = fieldsEnclosedBy;
            return this;
        }

        public FileMetaDataBuilder IgnoreHeaderLines(int ignoreHeaderLines)
        {
            file.IgnoreHeaderLines = ignoreHeaderLines;
            return this;
        }

        public FileMetaDataBuilder RowType(string rowType)
        {
            file.RowType = rowType;
            return this;
        }

        public FileMetaDataBuilder Encoding(Encoding encoding)
        {
            file.Encoding = encoding.WebName.ToUpper();
            return this;
        }

        public FileType Build()
        {
            return file;
        }
    }
}
