using DwC_A.Meta;
using DwC_A.Terms;
using System.Text;
using Xunit;

namespace Tests
{
    [Collection("ArchiveWriterCollection")]
    public class CoreFileMetaDataBuilderTests
    {
        private readonly ArchiveWriterFixture fixture;

        public CoreFileMetaDataBuilderTests(ArchiveWriterFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ShoudBuildFileMetaData()
        {
            var fieldsMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;

            var fileMetaData = CoreFileMetaDataBuilder.File("taxon.txt")
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Taxon)
                .AddFields(fieldsMetaDataBuilder)
                .Build();
            Assert.Equal("UTF-8", fileMetaData.Encoding);
        }
    }
}
