using DwC_A.Builders;
using DwC_A.Meta;
using DwC_A.Terms;
using DwC_A.Writers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    [Collection("ArchiveWriterCollection")]
    public class ArchiveWriterTests
    {
        private readonly ArchiveWriterFixture fixture;

        public ArchiveWriterTests(ArchiveWriterFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ShouldBuildArchive()
        {
            var context = BuilderContext.Default;

            var occurrences = await fixture.GetOccurrencesAsync();
            var occurrenceMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;
            var coreFileMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(occurrenceMetaDataBuilder);
            var coreFileMetaData = new CoreFileMetaData(coreFileMetaDataBuilder.Build());
            var coreFileBuilder = new FileBuilder(coreFileMetaData)
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

            var multimedia = await fixture.GetMultimediaAsync();
            var multimediaMetaDataBuilder = fixture.MultimediaMetaDataBuilder;
            var extensionFileMetaDataBuilder = ExtensionFileMetaDataBuilder.File("multimedia.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .CoreIndex(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(multimediaMetaDataBuilder);
            var extensionFileMetaData = new ExtensionFileMetaData(extensionFileMetaDataBuilder.Build());
            var extensionFileBuilder = new FileBuilder(extensionFileMetaData)
                .Context(context)
                .BuildRows(rowBuilder =>
                {
                    foreach (var media in multimedia)
                    {
                        rowBuilder.AddField(media.Id)
                                  .AddField(media.Type)
                                  .AddField(media.Format)
                                  .AddField(media.Identifier)
                                  .Build();
                    }
                });

            var archiveName = "archivexxx.zip";
            ArchiveWriter.CoreFile(coreFileBuilder, coreFileMetaDataBuilder)
                .Context(context)
                .AddExtensionFile(extensionFileBuilder, extensionFileMetaDataBuilder)
                .AddExtraFile("resources/ExtraData.txt")
                .Build(archiveName);

            Assert.True(File.Exists(archiveName));
            context.Cleanup();
        }
    }
}
