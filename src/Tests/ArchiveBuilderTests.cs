using DwC_A.Meta;
using DwC_A.Terms;
using DwC_A.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class ArchiveBuilderTests
    {
        class Taxon
        {
            public string Id { get; set; }
            public string ScientificName { get; set; }
        }

        readonly Taxon[] taxa = new[]
        {
            new Taxon
            {
                Id = "1234",
                ScientificName = "Platax orbicularis"
            },
            new Taxon
            {
                Id = "4567",
                ScientificName = "Pterois volitans"
            }
        };

        [Fact]
        public void ShouldBuildArchive()
        {
            var fieldsMetaDataBuilder = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(_ => _.Term(Terms.identificationID))
                .AddField(_ => _.Term(Terms.scientificName));

            var coreFile = CoreFileMetaDataBuilder.File("taxon.txt")
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Taxon)
                .AddFields(fieldsMetaDataBuilder);

            var extensionFieldsBuilder = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(_ => _.Term(Terms.identificationID))
                .AddField(_ => _.Term(Terms.sampleSizeValue));

            var extensionFile = ExtensionFileMetaDataBuilder.File("occurrent.txt")
                .CoreIndex(0)
                .RowType(RowTypes.Occurrence)
                .AddFields(extensionFieldsBuilder);

            var archiveMetaDataBuilder = new ArchiveMetaDataBuilder(".")
                .CoreFile(coreFile)
                .AddExtension(extensionFile);

            var coreFileMetaDataRaw = CoreFileMetaDataBuilder.File("taxon.txt")
                .IgnoreHeaderLines(1)
                .Encoding(Encoding.UTF8)
                .Index(0)
                .RowType(RowTypes.Taxon)
                .AddFields(fieldsMetaDataBuilder)
                .Build();

            var coreFileMetaData = new CoreFileMetaData(coreFileMetaDataRaw);
            var fileBuilder = new FileBuilder(coreFileMetaData);
            fileBuilder.BuildRows(rowBuilder =>
            {
                foreach (var taxon in taxa)
                {
                    rowBuilder.AddField(taxon.Id)
                              .AddField(taxon.ScientificName)
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
