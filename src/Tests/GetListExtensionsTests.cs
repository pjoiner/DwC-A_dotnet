using DwC_A;
using DwC_A.Builders;
using DwC_A.Exceptions;
using DwC_A.Extensions;
using DwC_A.Meta;
using DwC_A.Terms;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class GetListExtensionsTests
    {
        private readonly Mock<IRow> mockRow = new();

        public GetListExtensionsTests() 
        {
            string list = "COORDINATE_ROUNDED;GEODETIC_DATUM_ASSUMED_WGS84;INSTITUTION_MATCH_NONE;COLLECTION_MATCH_NONE";
            string taxonID = "1234";
            var metaData = FieldsMetaDataBuilder.Fields()
                .AutomaticallyIndex()
                .AddField(m => m.Term("issues").Delimiter(";"))
                .AddField(m => m.Term(Terms.taxonID))
                .Build();
            var meta = new FieldMetaData(null, metaData);
            mockRow.Setup(r => r.FieldMetaData).Returns(meta);
            mockRow.Setup(r => r[0]).Returns(list);
            mockRow.Setup(r => r["issues"]).Returns(list);
            mockRow.Setup(r => r.TryGetField("issues", out list)).Returns(true);
            mockRow.Setup(r => r[1]).Returns(taxonID);
            mockRow.Setup(r => r[Terms.taxonID]).Returns(taxonID);
            mockRow.Setup(r => r.TryGetField(Terms.taxonID, out taxonID)).Returns(true);
        }

        [Fact]
        public void ShouldReturnListOfIssues()
        {
            var row = mockRow.Object;

            AssertGetListOfIssues(row.GetListOf("issues"));
            AssertGetListOfIssues(row.GetListOf(0));
        }

        [Fact]
        public void TryGetListShouldReturnListOfIssues()
        {
            var row = mockRow.Object;

            Assert.True(row.TryGetListOf("issues", out var list));
            AssertGetListOfIssues(list);
        }

        private static void AssertGetListOfIssues(IEnumerable<string> issues)
        {
            Assert.Collection(issues,
                issue => Assert.Equal("COORDINATE_ROUNDED", issue),
                issue => Assert.Equal("GEODETIC_DATUM_ASSUMED_WGS84", issue),
                issue => Assert.Equal("INSTITUTION_MATCH_NONE", issue),
                issue => Assert.Equal("COLLECTION_MATCH_NONE", issue));
        }

        [Fact]
        public void ShouldReturnListWithOneItemOnNoDelimiter()
        {
            var row = mockRow.Object;

            Assert.Collection(row.GetListOf(Terms.taxonID),
                item => Assert.Equal("1234", item));
            Assert.Collection(row.GetListOf(1),
                item => Assert.Equal("1234", item));
        }

        [Fact]
        public void ShouldThrowOnTermNotFound()
        {
            var row = mockRow.Object;

            Assert.Throws<TermNotFoundException>(() => row.GetListOf(Terms.decimalLatitude));
        }

        [Fact]
        public void ShouldReturnFalseOnTermNotFound()
        {
            var row = mockRow.Object;

            Assert.False(row.TryGetListOf(Terms.decimalLatitude, out var _));
        }
    }
}
