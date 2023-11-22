using DwC_A.Builders;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace DwC_A.Writers
{
    /// <summary>
    /// Writes data and compresses files to build an archive.
    /// </summary>
    public class ArchiveWriter
    {
        private readonly ArchiveMetaDataBuilder archiveMetaDataBuilder;
        private FileBuilder coreFileBuilder;
        private readonly IList<FileBuilder> extensionFileBuilders = new List<FileBuilder>();
        private readonly IList<string> extraFiles = new List<string>();
        private BuilderContext context;

        protected ArchiveWriter(FileBuilder coreFileBuilder, CoreFileMetaDataBuilder coreFileMetaDataBuilder)
        {
            this.coreFileBuilder = coreFileBuilder;
            archiveMetaDataBuilder = ArchiveMetaDataBuilder.CoreFile(coreFileMetaDataBuilder);
        }

        /// <summary>
        /// Creates an archive writer to build and compress files.
        /// </summary>
        /// <param name="coreFileBuilder">FileBuilder for the core file.</param>
        /// <param name="coreFileMetaDataBuilder">Metadata builder for the core file.</param>
        /// <returns>ArchiveWriter</returns>
        public static ArchiveWriter CoreFile(FileBuilder coreFileBuilder, CoreFileMetaDataBuilder coreFileMetaDataBuilder)
        {
            return new ArchiveWriter(coreFileBuilder, coreFileMetaDataBuilder);
        }

        /// <summary>
        /// Adds the FileBuilder and metadata builder for an extension file.
        /// </summary>
        /// <param name="extension">Extension FileBuilder</param>
        /// <param name="extensionFileMetaDataBuilder">Extension metadata builder</param>
        /// <returns>this</returns>
        public ArchiveWriter AddExtensionFile(FileBuilder extension, ExtensionFileMetaDataBuilder extensionFileMetaDataBuilder)
        {
            archiveMetaDataBuilder.AddExtension(extensionFileMetaDataBuilder);
            extensionFileBuilders.Add(extension);
            return this;
        }

        /// <summary>
        /// Pass a directory context to determine where files are written.  If no context is passed then a default context will be used in a temporary directory.
        /// </summary>
        /// <param name="context">Directory context</param>
        /// <returns>this</returns>
        public ArchiveWriter Context(BuilderContext context)
        {
            this.context = context;
            return this;
        }

        /// <summary>
        /// Add any extra files, such as, eml.xml or license.txt using this method.
        /// </summary>
        /// <param name="fileName">Absolute or relative path to file</param>
        /// <returns>this</returns>
        public ArchiveWriter AddExtraFile(string fileName)
        {
            extraFiles.Add(fileName);
            return this;
        }

        /// <summary>
        /// Builds the file.
        /// </summary>
        /// <param name="archiveFileName">Pass filename only</param>
        public void Build(string archiveFileName)
        {
            var metaDataFileName = archiveMetaDataBuilder
                .Context(context)
                .Serialize();
            using(var stream = new FileStream(archiveFileName, FileMode.Create))
            {
                using(var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false))
                {
                    zipArchive.CreateEntryFromFile(metaDataFileName, archiveMetaDataBuilder.FileName);
                    zipArchive.CreateEntryFromFile(coreFileBuilder.Build(), coreFileBuilder.FileName);
                    foreach(var extensionFileBuilder in extensionFileBuilders)
                    {
                        zipArchive.CreateEntryFromFile(extensionFileBuilder.Build(), extensionFileBuilder.FileName);
                    }
                    foreach(var extraFile in extraFiles)
                    {
                        zipArchive.CreateEntryFromFile(extraFile, extraFile);
                    }
                }
            }
        }
    }
}
