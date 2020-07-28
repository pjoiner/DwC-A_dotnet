using DwC_A.Meta;
using DwC_A.Terms;
using Xunit;

namespace Tests
{
    public class FieldMetaDataBuilderTests
    {
        [Fact]
        public void ShouldBuildFieldMetaData()
        {
            var fieldMetaData = FieldMetaDataBuilder.Field()
                .Index(0)
                .Term(Terms.Identification)
                .Build();
            Assert.NotNull(fieldMetaData.Term);
            Assert.Equal(0, fieldMetaData.Index);
        }

        [Fact]
        public void ShouldBuildFieldMetaDataThroughCstor()
        {
            var fieldMetaData = FieldMetaDataBuilder.Field(Terms.Identification)
                .Index(0)
                .Build();
            Assert.Equal(Terms.Identification, fieldMetaData.Term);
        }

        [Fact]
        public void ShouldBuildFieldMetaDataArray()
        {
            var fieldsMetaData = FieldsMetaDataBuilder.Fields()
                    .AddField(_ => _.Index(0).Term(Terms.identificationID))
                    .AddField(_ => _.Index(1).Term(Terms.scientificName))
                    .Build();
            Assert.NotEmpty(fieldsMetaData);
        }

        [Fact]
        public void ShouldBuildFieldMetaDataArrayWithIndex()
        {
            var fieldsMetaData = FieldsMetaDataBuilder.Fields()
                    .AutomaticallyIndex()
                    .AddField(_ => _.Term(Terms.identificationID))
                    .AddField(_ => _.Term(Terms.scientificName))
                    .Build();
            Assert.NotEmpty(fieldsMetaData);
        }

    }
}
