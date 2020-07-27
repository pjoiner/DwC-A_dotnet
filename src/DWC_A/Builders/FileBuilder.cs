using DwC_A.Meta;
using System;
using System.IO;

namespace DwC_A.Builders
{
    public class FileBuilder
    {
        private readonly IFileMetaData fileMetaData;
        private Action<RowBuilder> row;
        private BuilderContext context;

        public FileBuilder(IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData;
        }

        public string FileName => fileMetaData.FileName;
        public string FullFileName { get; private set; }

        private FileBuilder AddHeader(TextWriter writer)
        {
            var rowBuilder = new RowBuilder(fileMetaData, writer);
            foreach (var field in fileMetaData.Fields)
            {
                rowBuilder.AddField(Terms.Terms.ShortName(field.Term));
            }
            writer.WriteLine(rowBuilder.BuildString());
            return this;
        }

        private string GetPath()
        {
            if(context == null)
            {
                return BuilderContext.Default.Path;
            }
            return context.Path;
        }

        public FileBuilder Context(BuilderContext context)
        {
            this.context = context;
            return this;
        }

        public FileBuilder BuildRows(Action<RowBuilder> row)
        {
            this.row = row;
            return this;
        }

        public string Build()
        {
            FullFileName = Path.Combine(GetPath(), fileMetaData.FileName);
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
