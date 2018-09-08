using DWC_A.Meta;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Xunit;

namespace Tests
{
    public class MetaDataTests
    {
        ILogger debugLogger = new DebugLogger(typeof(MetaDataReader).Name);

        [Fact]
        public void ArchiveShouldContainCoreFile()
        {
            var metaDataReader = new MetaDataReader(debugLogger);
            var archive = metaDataReader.ReadMetaData("./resources/dwca-vascan-v37.5");
            Assert.NotNull(archive);
        }
    }
}
