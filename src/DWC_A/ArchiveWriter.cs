using DwC_A.Builders;
using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace DwC_A.Writers
{
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

        public static ArchiveWriter CoreFile(FileBuilder coreFileBuilder, CoreFileMetaDataBuilder coreFileMetaDataBuilder)
        {
            return new ArchiveWriter(coreFileBuilder, coreFileMetaDataBuilder);
        }

        public ArchiveWriter AddExtensionFile(FileBuilder extension, ExtensionFileMetaDataBuilder extensionFileMetaDataBuilder)
        {
            archiveMetaDataBuilder.AddExtension(extensionFileMetaDataBuilder);
            extensionFileBuilders.Add(extension);
            return this;
        }

        public ArchiveWriter Context(BuilderContext context)
        {
            this.context = context;
            return this;
        }

        public ArchiveWriter AddExtraFile(string fileName)
        {
            extraFiles.Add(fileName);
            return this;
        }

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
