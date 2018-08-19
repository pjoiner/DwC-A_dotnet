using DwC_A.Meta;
using Xunit;

namespace Tests
{
    public class MetaDataTests
    {

        [Fact]
        public void ArchiveShouldContainCoreFile()
        {
            var metaDataReader = new MetaDataReader();
            var archive = metaDataReader.ReadMetaData("./resources/dwca-vascan-v37.5");
            Assert.NotNull(archive);
        }
    }
}
