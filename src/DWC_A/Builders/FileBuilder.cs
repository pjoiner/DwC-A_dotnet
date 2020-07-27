using DwC_A.Meta;
using System;
using System.IO;

namespace DwC_A.Builders
{
    public class FileBuilder
    {
        private readonly IFileMetaData fileMetaData;

        public FileBuilder(IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData;
        }

        public string FileName => fileMetaData.FileName;
        public string FullFileName { get; private set; }

        public FileBuilder AddHeader(TextWriter writer)
        {
            var rowBuilder = new RowBuilder(fileMetaData, writer);
            foreach (var field in fileMetaData.Fields)
            {
                rowBuilder.AddField(Terms.Terms.ShortName(field.Term));
            }
            writer.WriteLine(rowBuilder.BuildString());
            return this;
        }

        public string BuildRows(Action<RowBuilder> row)
        {
            FullFileName = Path.Combine(ArchiveBuilderHelper.Path, fileMetaData.FileName);
            using (var stream = new FileStream(FullFileName, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream, fileMetaData.Encoding))
                {
                    var rowBuilder = new RowBuilder(fileMetaData, writer);
                    writer.NewLine = fileMetaData.LinesTerminatedBy;
                    if (fileMetaData.HeaderRowCount > 0)
                    {
                        AddHeader(writer);
                    }
                    row(rowBuilder);
                }
            }
            return FullFileName;
        }

    }
}
