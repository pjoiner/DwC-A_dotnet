using DwC_A;
using DwC_A.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ArchiveReaderTests
    {
        private const string archiveFileName = "./resources/dwca-vascan-v37.5.zip";
        private const string archiveFolder = "./resources/dwca-vascan-v37.5";
        private const string whalesArchiveFileName = "./resources/whales.zip";

        [Fact]
        public void ShouldOpenCoreFile()
        {
            using var archive = new ArchiveReader(archiveFileName);
            foreach (var row in archive.CoreFile.Rows)
            {
                Assert.NotNull(row[0]);
                Assert.NotNull(row["id"]);
            }
        }

        [Fact]
        public async Task ShouldOpenCoreFileAsync()
        {
            using var archive = new ArchiveReader(archiveFileName);
            await foreach (var row in archive
                .GetAsyncCoreFile()
                .GetDataRowsAsync(TestContext.Current.CancellationToken))
            {
                Assert.NotNull(row[0]);
            }
        }

        [Fact]
        public void ShouldReturnDescriptionExtensionFile()
        {
            using var archive = new ArchiveReader(archiveFileName);
            var descriptionFile = archive.Extensions.GetFileReaderByFileName("description.txt");
            Assert.NotEmpty(descriptionFile.Rows);
        }

        [Fact]
        public async Task ShouldReturnDescriptionExtensionFileAsync()
        {
            using var archive = new ArchiveReader(archiveFileName);
            var descriptionFile = archive.Extensions.GetAsyncFileReaderByFileName("description.txt");
            Assert.NotEmpty(await descriptionFile
                .GetDataRowsAsync(TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
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

        [Fact]
        public void ShouldOpenWhalesArchive()
        {
            using var whales = new ArchiveReader(whalesArchiveFileName);
            Assert.NotEmpty(whales.CoreFile.Rows);
            Assert.Empty(whales.CoreFile.FileMetaData.Fields);
            Assert.Equal(4, whales.CoreFile.DataRows.Count());
        }

        [Fact]
        public void ShouldThrowFileOrPathNotFound()
        {
            Assert.Throws<InvalidArchiveException>(() => new ArchiveReader("./resources"));
        }
    }
}
