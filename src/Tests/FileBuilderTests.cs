using DwC_A.Builders;
using DwC_A.Meta;
using DwC_A.Terms;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    [Collection("ArchiveWriterCollection")]
    public class FileBuilderTests
    {
        private readonly ArchiveWriterFixture fixture;

        public FileBuilderTests(ArchiveWriterFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ShouldBuildOccurrenceFile()
        {
            var occurrences = await fixture.GetOccurrencesAsync();
            var fieldsMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;
            var context = new BuilderContext(".");

            var occurrenceMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(fieldsMetaDataBuilder)
                .Build();

            var coreFileMetaData = new CoreFileMetaData(occurrenceMetaDataBuilder);
            var fileBuilder = FileBuilder.MetaData(coreFileMetaData)
                .Context(context)
                .BuildRows(rowBuilder =>
                {
                    foreach (var occurrence in occurrences)
                    {
                        rowBuilder.AddField(occurrence.OccurrenceID)
                                    .AddField(occurrence.BasisOfRecord)
                                    .AddField(occurrence.ScientificName)
                                    .AddField(occurrence.EventDate.ToString("yyyy-MM-dd"))
                                    .AddField(occurrence.DecimalLatitude)
                                    .AddField(occurrence.DecimalLongitude)
                                    .AddField(occurrence.GeodeticDatum)
                                    .Build();
                    }
                });
            var fileName = fileBuilder.Build();
            Assert.True(File.Exists(fileName));
        }
    }
}
