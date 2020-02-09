using DwC_A.Meta;
using System.Text;
using Xunit;

namespace Tests
{
    public class FileMetaDataBuilderTests
    {
        [Fact]
        public void ShoudBuildFileMetaData()
        {
            var fileMetaData = FileMetaDataBuilder.File("taxon.txt")
                .Encoding(Encoding.UTF8)
                .Build();
            Assert.Equal("UTF-8", fileMetaData.Encoding);
        }
    }
}
