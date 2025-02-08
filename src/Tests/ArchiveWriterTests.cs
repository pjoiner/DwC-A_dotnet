using DwC_A;
using DwC_A.Builders;
using DwC_A.Terms;
using DwC_A.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [Collection("ArchiveWriterCollection")]
    public class ArchiveWriterTests(ArchiveWriterFixture fixture)
    {
        private readonly ArchiveWriterFixture fixture = fixture;

        [Fact]
        public void ShouldBuildArchiveFromExistingFile()
        {
            var fieldMetaDataBuilder = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(_ => _.Term(Terms.taxonID))
                .AddField(_ => _.Term(Terms.vernacularName))
                .AddField(_ => _.Term(Terms.language));
            var fileMetaData = CoreFileMetaDataBuilder.File("taxon.txt")
                .FieldsEnclosedBy("\"")
                .FieldsTerminatedBy(",")
                .LinesTerminatedBy("\\n")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Taxon)
                .AddFields(fieldMetaDataBuilder);
            var fileBuider = FileBuilder.MetaData(fileMetaData)
                .UseExistingFile("./resources/whales/whales.txt");
            ArchiveWriter.CoreFile(fileBuider, fileMetaData)
                .Build("whales.zip");

            Assert.True(File.Exists("whales.zip"));

            using var archive = new ArchiveReader("whales.zip");
            var whales = archive.CoreFile
                .DataRows
                .Select(n => n[Terms.vernacularName]);
            Assert.Equal(["sperm whale", "cachalot", "gray whale"], whales);
        }

        [Fact]
        public async Task ShouldBuildArchive()
        {
            var context = new BuilderContext(Guid.NewGuid().ToString(), true);

            var occurrences = await fixture.GetOccurrencesAsync();
            var occurrenceMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;
            var coreFileMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(occurrenceMetaDataBuilder);
            var coreFileBuilder = FileBuilder.MetaData(coreFileMetaDataBuilder)
                .Context(context)
                .BuildRows(rowBuilder => BuildCoreRows(rowBuilder));

            var multimedia = await fixture.GetMultimediaAsync();
            var multimediaMetaDataBuilder = fixture.MultimediaMetaDataBuilder;
            var extensionFileMetaDataBuilder = ExtensionFileMetaDataBuilder.File("multimedia.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .CoreIndex(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(multimediaMetaDataBuilder);
            var extensionFileBuilder = FileBuilder.MetaData(extensionFileMetaDataBuilder)
                .Context(context)
                .BuildRows(rowBuilder => BuildExtensionRows(rowBuilder));

            var archiveName = "archivexxx.zip";
            ArchiveWriter.CoreFile(coreFileBuilder, coreFileMetaDataBuilder)
                .Context(context)
                .AddExtensionFile(extensionFileBuilder, extensionFileMetaDataBuilder)
                .AddExtraFile("resources/ExtraData.txt")
                .Build(archiveName);

            Assert.True(File.Exists(archiveName));
            context.Cleanup();
        }

        [Fact]
        public async Task ShouldBuildCoreFile()
        {
            var context = new BuilderContext(Guid.NewGuid().ToString(), true);

            var occurrences = await fixture.GetOccurrencesAsync();
            var occurrenceMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;
            var coreFileMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .FieldsEnclosedBy("")
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(occurrenceMetaDataBuilder);
            var coreFileBuilder = FileBuilder.MetaData(coreFileMetaDataBuilder)
                .Context(context)
                .BuildRows(rowBuilder => BuildCoreRows(rowBuilder));
            var fileName = coreFileBuilder.Build();

            var expected = fixture.ExpectedCoreFileHeader;
            Assert.Equal(expected, File.ReadAllLines(fileName)[0]);
        }

        [Fact]
        public async Task ShouldBuildCoreFileWithCustomHeader()
        {
            var context = new BuilderContext(Guid.NewGuid().ToString(), true);

            var occurrences = await fixture.GetOccurrencesAsync();
            var occurrenceMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;
            var coreFileMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(2)
                .Encoding(Encoding.UTF8)
                .FieldsEnclosedBy("")
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(occurrenceMetaDataBuilder);
            var coreFileBuilder = FileBuilder.MetaData(coreFileMetaDataBuilder)
                .Context(context)
                .AddCustomHeader(BuildCustomCoreFileHeader)
                .BuildRows(rowBuilder => BuildCoreRows(rowBuilder));
            var fileName = coreFileBuilder.Build();

            var actual = File.ReadAllLines(fileName);
            Assert.Equal("Custom core file", actual[0]);
            Assert.Equal(fixture.ExpectedCoreFileHeader, actual[1]);
        }

        [Fact]
        public async Task FileBuilderShouldThrowOnWrongNumberOfHeaderLines()
        {
            var context = new BuilderContext(Guid.NewGuid().ToString(), true);

            var occurrences = await fixture.GetOccurrencesAsync();
            var occurrenceMetaDataBuilder = fixture.OccurrenceFieldsMetaDataBuilder;
            var coreFileMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .FieldsEnclosedBy("")
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(occurrenceMetaDataBuilder);
            var coreFileBuilder = FileBuilder.MetaData(coreFileMetaDataBuilder)
                .Context(context)
                .AddCustomHeader(BuildCustomCoreFileHeader)
                .BuildRows(rowBuilder => BuildCoreRows(rowBuilder));
            Assert.Throws<InvalidOperationException>(() => coreFileBuilder.Build());
        }

        private IEnumerable<string> BuildCustomCoreFileHeader()
        {
            yield return "Custom core file";
            yield return fixture.ExpectedCoreFileHeader;
        }

        private IEnumerable<string> BuildCoreRows(RowBuilder rowBuilder)
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
                          .Build();
            }
        }

        private IEnumerable<string> BuildExtensionRows(RowBuilder rowBuilder)
        {
            var multimedia = fixture.GetMultimediaAsync().Result;
            foreach (var media in multimedia)
            {
                yield return rowBuilder.AddField(media.Id)
                          .AddField(media.Type)
                          .AddField(media.Format)
                          .AddField(media.Identifier)
                          .Build();
            }
        }
    }
}
