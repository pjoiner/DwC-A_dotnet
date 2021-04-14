using DwC_A;
using DwC_A.Config;
using System.IO;
using Xunit;

namespace Tests
{
    public class ArchiveFolderTests
    {
        private const string archiveFileName = "./resources/dwca-vascan-v37.5.zip";

        [Fact]
        public void ShouldOpenNewInstanceInAnotherDirectory()
        {
            var defaultConfig = new ArchiveFolderConfiguration();
            var archiveFolder1 = new ArchiveFolder(archiveFileName, defaultConfig);
            var first = archiveFolder1.Extract();
            var archiveFolder2 = new ArchiveFolder(archiveFileName, defaultConfig);
            var second = archiveFolder2.Extract();
            archiveFolder1.DeleteFolder();
            archiveFolder2.DeleteFolder();
            Assert.NotEqual(first, second);
        }

        [Fact]
        public void ShouldKeepArchive()
        {
            var customConfig = new ArchiveFolderConfiguration()
            {
                OutputPath = "./CustomFolder",
                ShouldCleanup = false
            };
            if (Directory.Exists(customConfig.OutputPath))
            {
                Directory.Delete(customConfig.OutputPath, true);
            }
            var archiveFolder = new ArchiveFolder(archiveFileName, customConfig);
            var outputPath = archiveFolder.Extract();
            archiveFolder.DeleteFolder();
            Assert.True(Directory.Exists(customConfig.OutputPath));
        }

        [Fact]
        public void ShouldAllowArchiveOverwrite()
        {
            var whales = "./resources/whales.zip";
            var defaultConfig = new ArchiveFolderConfiguration()
            {
                OutputPath = "./whales",
                ShouldCleanup = false,
                Overwrite = true
            };
            var archiveFolder = new ArchiveFolder(whales, defaultConfig);
            archiveFolder.Extract();
            archiveFolder = new ArchiveFolder(whales, defaultConfig);
            var action = Record.Exception(() => archiveFolder.Extract());
            Assert.Null(action);
        }

        [Fact]
        public void ShouldExtractRecursively()
        {
            var whales = @"./resources/whales.zip";
            var config = new ArchiveFolderConfiguration()
            {
                OutputPath = "./whales",
                Overwrite = true,
                ShouldCleanup = false
            };
            var archiveFolder = new ArchiveFolder(whales, config);
            var action = Record.Exception(() => archiveFolder.Extract());
            Assert.Null(action);
            Assert.True(File.Exists("./whales/extra/extra.txt"));
        }
    }
}
