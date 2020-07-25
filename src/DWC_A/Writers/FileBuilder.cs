using DwC_A.Meta;
using DwC_A.Terms;
using System;
using System.IO;

namespace DwC_A.Writers
{
    public class FileBuilder
    {
        private readonly IFileMetaData fileMetaData;

        public FileBuilder(IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData;
        }

        public string FileName => fileMetaData.FileName;

        public FileBuilder AddHeader(TextWriter writer)
        {
            var rowBuilder = new RowBuilder(fileMetaData, writer);
            foreach(var field in fileMetaData.Fields)
            {
                rowBuilder.AddField(Terms.Terms.ShortName(field.Term));
            }
            writer.WriteLine(rowBuilder.BuildString());
            return this;
        }

        public void BuildRows( Action<RowBuilder> row )
        {
            using (var stream = new FileStream(fileMetaData.FileName, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream, fileMetaData.Encoding))
                {
                    var rowBuilder = new RowBuilder(fileMetaData, writer);
                    writer.NewLine = fileMetaData.LinesTerminatedBy;
                    if(fileMetaData.HeaderRowCount > 0)
                    {
                        AddHeader(writer);
                    }
                    row(rowBuilder);
                }
            }
        }

    }
}
