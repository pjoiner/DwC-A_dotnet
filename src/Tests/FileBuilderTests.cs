using DwC_A.Builders;
using DwC_A.Meta;
using DwC_A.Terms;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        public void ShouldBuildOccurrenceFile()
        {
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
                .BuildRows(rowBuilder => BuildOccurrenceRows(rowBuilder));
            var fileName = fileBuilder.Build();
            Assert.True(File.Exists(fileName));
        }

        private IEnumerable<string> BuildOccurrenceRows(RowBuilder rowBuilder)
        {
            var occurrences = fixture.GetOccurrencesAsync().Result;
            foreach (var occurrence in occurrences)
            {
                yield return rowBuilder.AddField(occurrence.OccurrenceID)
                            .AddField(occurrence.BasisOfRecord)
                            .AddField(occurrence.ScientificName)
                            .AddField(occurrence.EventDate.ToString("yyyy-MM-dd"))
                            .AddField(occurrence.DecimalLatitude)
                            .AddField(occurrence.DecimalLongitude)
                            .AddField(occurrence.GeodeticDatum)
                            .Build();
            }
        }
    }
}
