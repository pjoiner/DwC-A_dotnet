using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace DwC_A.Writers
{
    public class ArchiveBuilder
    {
        private readonly ArchiveMetaDataBuilder archiveMetaDataBuilder;
        private readonly FileBuilder coreFileBuilder;
        private readonly IList<FileBuilder> extensionFileBuilders = new List<FileBuilder>();
        private readonly IList<string> extraFiles = new List<string>();

        public ArchiveBuilder(ArchiveMetaDataBuilder archiveMetaDataBuilder, FileBuilder coreFileBuilder)
        {
            this.archiveMetaDataBuilder = archiveMetaDataBuilder;
            this.coreFileBuilder = coreFileBuilder;
        }

        public ArchiveBuilder AddExtensionFileBuilder(FileBuilder extension)
        {
            extensionFileBuilders.Add(extension);
            return this;
        }

        public ArchiveBuilder AddExtraFile(string fileName)
        {
            extraFiles.Add(fileName);
            return this;
        }

        public void Build(string archiveFileName)
        {
            const string metaDataFileName = "meta.xml";
            archiveMetaDataBuilder.Serialize();
            using(var stream = new FileStream(archiveFileName, FileMode.Create))
            {
                using(var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false))
                {
                    zipArchive.CreateEntryFromFile(metaDataFileName, metaDataFileName);
                    zipArchive.CreateEntryFromFile(coreFileBuilder.FileName, coreFileBuilder.FileName);
                    foreach(var extensionFileBuilder in extensionFileBuilders)
                    {
                        zipArchive.CreateEntryFromFile(extensionFileBuilder.FileName, extensionFileBuilder.FileName);
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
