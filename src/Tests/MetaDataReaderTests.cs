using DwC_A.Meta;
using System.Linq;
using Xunit;

namespace Tests
{
    public class MetaDataReaderTests
    {
        [Fact]
        public void ShouldReturnDefaultMetaData()
        {
            var whales = "./resources/whales";
            var metaDataReader = new MetaDataReader();
            var metaData = metaDataReader.ReadMetaData(whales);

            Assert.Equal("whales.txt", metaData.Core.Files.FirstOrDefault());
            Assert.Equal("\\n", metaData.Core.LinesTerminatedBy);
        }
    }
}
