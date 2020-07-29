using DwC_A.Meta;
using DwC_A.Terms;
using System.Text;
using Xunit;

namespace Tests
{
    [Collection("ArchiveWriterCollection")]
    public class ExtensionFileMetadataBuilderTests
    {
        private readonly ArchiveWriterFixture fixture;

        public ExtensionFileMetadataBuilderTests(ArchiveWriterFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ShouldBuildExtensionFile()
        {
            var multimediaMetaDataBuilder = fixture.MultimediaMetaDataBuilder;

            var fileMetaData = ExtensionFileMetaDataBuilder.File("multimedia.txt")
                .Encoding(Encoding.UTF8)
                .CoreIndex(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(multimediaMetaDataBuilder)
                .Build();
            Assert.Equal("UTF-8", fileMetaData.Encoding);
            Assert.Equal(0, fileMetaData.Coreid.Index);
        }
    }
}
