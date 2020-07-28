using DwC_A.Meta;
using System;
using System.IO;

namespace DwC_A.Builders
{
    /// <summary>
    /// This class is used to write data to a file including header information.
    /// </summary>
    public class FileBuilder
    {
        private readonly IFileMetaData fileMetaData;
        private Action<RowBuilder> row;
        private BuilderContext context;

        private FileBuilder(IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData;
        }

        /// <summary>
        /// Filename of file.
        /// </summary>
        public string FileName => fileMetaData.FileName;
        /// <summary>
        /// Full filename of file include path.
        /// </summary>
        public string FullFileName { get; private set; }

        /// <summary>
        /// Use this method to create new FileBuilder object with supplied metadata
        /// </summary>
        /// <param name="fileMetaData"></param>
        /// <returns></returns>
        public static FileBuilder MetaData(IFileMetaData fileMetaData)
        {
            return new FileBuilder(fileMetaData);
        }

        private FileBuilder AddHeader(TextWriter writer)
        {
            var rowBuilder = new RowBuilder(fileMetaData, writer);
            foreach (var field in fileMetaData.Fields)
            {
                rowBuilder.AddField(Terms.Terms.ShortName(field.Term));
            }
            rowBuilder.Build();
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

        /// <summary>
        /// Pass a BuilderContext object to this method to indicate where the file should be written.
        /// Note: Always use the same context for all FileBuilders, MetaDataBuilder and ArchiveWriter.
        /// </summary>
        /// <param name="context">Context class containing path information.</param>
        /// <returns>this</returns>
        public FileBuilder Context(BuilderContext context)
        {
            this.context = context;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">Implement a delegate or lambda to iterate through data and build rows for the file.  
        /// Call the Build() method on each row using the supplied RowBuilder object.</param>
        /// <returns>this</returns>
        public FileBuilder BuildRows(Action<RowBuilder> row)
        {
            this.row = row;
            return this;
        }

        /// <summary>
        /// Build the file and write to disk.
        /// </summary>
        /// <returns>Filename of the file including the path.</returns>
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
