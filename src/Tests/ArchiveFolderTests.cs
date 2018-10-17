using DwC_A;
using Xunit;

namespace Tests
{
    public class ArchiveFolderTests
    {
        private const string archiveFileName = "./resources/dwca-vascan-v37.5.zip";

        [Fact]
        public void ShouldOpenNewInstanceInAnotherDirectory()
        {
            var archiveFolder1 = new ArchiveFolder(archiveFileName);
            var first = archiveFolder1.Extract();
            var archiveFolder2 = new ArchiveFolder(archiveFileName);
            var second = archiveFolder2.Extract();
            archiveFolder1.DeleteFolder();
            archiveFolder2.DeleteFolder();
            Assert.NotEqual(first, second);
        }
    }
}
