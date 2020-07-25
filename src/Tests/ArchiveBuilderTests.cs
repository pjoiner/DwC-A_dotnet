using DwC_A.Meta;
using DwC_A.Terms;
using DwC_A.Writers;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ArchiveBuilderTests
    {
        class Occurrence
        {
            public Guid OccurrenceID { get; set; }
            public string BasisOfRecord { get; set; }
            public string ScientificName {get; set;}
            public DateTime EventDate { get; set; }
            public double DecimalLatitude { get; set; }
            public double DecimalLongitude { get; set; }
            public string GeodeticDatum { get; set; }
        }

        [Fact]
        public async Task ShouldBuildArchive()
        {
            Occurrence[] occurrences;
            using (var fs = File.OpenRead("./resources/occurrenceData.json"))
            {
                occurrences = await JsonSerializer.DeserializeAsync<Occurrence[]>(fs);
            }
            var fieldsMetaDataBuilder = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(_ => _.Term(Terms.occurrenceID))
                .AddField(_ => _.Term(Terms.basisOfRecord))
                .AddField(_ => _.Term(Terms.scientificName))
                .AddField(_ => _.Term(Terms.eventDate))
                .AddField(_ => _.Term(Terms.decimalLatitude))
                .AddField(_ => _.Term(Terms.decimalLongitude))
                .AddField(_ => _.Term(Terms.geodeticDatum));

            var coreFileMetaDataBuilder = CoreFileMetaDataBuilder.File("occurrence.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(fieldsMetaDataBuilder);

            var coreFileMetaData = new CoreFileMetaData(coreFileMetaDataBuilder.Build());

            var archiveMetaDataBuilder = new ArchiveMetaDataBuilder(".")
                .CoreFile(coreFileMetaDataBuilder);

            var fileBuilder = new FileBuilder(coreFileMetaData);
            fileBuilder.BuildRows(rowBuilder =>
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

            var archiveBuilder = new ArchiveBuilder(archiveMetaDataBuilder, fileBuilder);
            archiveBuilder
                .AddExtraFile("resources/ExtraData.txt")
                .Build("archivexxx.zip");
        }
    }
}
