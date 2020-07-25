using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;

namespace DwC_A.Writers
{
    public class RowBuilder
    {
        private readonly IFileMetaData fileMetaData;
        private IList<string> fields = new List<string>();
        private readonly TextWriter textWriter;

        public RowBuilder(IFileMetaData fileMetaData, TextWriter textWriter)
        {
            this.fileMetaData = fileMetaData;
            this.textWriter = textWriter;
        }

        public RowBuilder AddField(object field)
        {
            fields.Add(fileMetaData.FieldsEnclosedBy + field.ToString() + fileMetaData.FieldsEnclosedBy);
            return this;
        }

        public string BuildString()
        {
            return string.Join(fileMetaData.FieldsTerminatedBy, fields);
        }

        public void Build()
        {
            textWriter.WriteLine(BuildString());
            fields.Clear();
        }
    }
}
