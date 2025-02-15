﻿using DwC_A.Builders;
using DwC_A.Terms;
using System.Text;
using Xunit;

namespace Tests
{
    public class ArchiveMetaDataBuilderTests
    {
        [Fact]
        public void ShouldBuildMetaFile()
        {
            var context = new BuilderContext(".");

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

            ArchiveMetaDataBuilder.CoreFile(coreFile)
                .Context(context)
                .AddExtension(extensionFile)
                .Serialize();
        }
    }
}
