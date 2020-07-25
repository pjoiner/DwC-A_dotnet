using DwC_A.Meta;
using DwC_A.Terms;
using System.Text;
using Xunit;

namespace Tests
{
    public class CoreFileMetaDataBuilderTests
    {
        [Fact]
        public void ShoudBuildFileMetaData()
        {
            var fieldsMetaDataBuilder = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(_ => _.Term(Terms.identificationID))
                .AddField(_ => _.Term(Terms.scientificName));

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
