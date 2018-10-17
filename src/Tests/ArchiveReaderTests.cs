using DwC_A;
using System;
using Xunit;

namespace Tests
{
    public class ArchiveReaderTests
    {
        private const string archiveFileName = "./resources/dwca-vascan-v37.5.zip";
        private const string archiveFolder = "./resources/dwca-vascan-v37.5";

        [Fact]
        public void ShouldOpenCoreFile()
        {
            using (var archive = new ArchiveReader(archiveFileName))
            {
                foreach(var row in archive.CoreFile.Rows)
                {
                    Assert.NotNull(row[0]);
                }
            }
        }

        [Fact]
        public void ShouldReturnDescriptionExtensionFile()
        {
            using (var archive = new ArchiveReader(archiveFileName))
            {
                var descriptionFile = archive.Extensions.GetFileReaderByFileName("description.txt");
                Assert.NotEmpty(descriptionFile.Rows);
            }
        }

        [Fact]
        public void ShouldThrowOnNullName()
        {
            Assert.Throws<ArgumentNullException>(() => new ArchiveReader(null, null));
        }

        [Fact]
        public void ShouldNotThrowOnDispose()
        {
            var archive = new ArchiveReader(archiveFolder);
            var exception = Record.Exception(() => archive.Dispose());
            Assert.Null(exception);
        }

    }
}
