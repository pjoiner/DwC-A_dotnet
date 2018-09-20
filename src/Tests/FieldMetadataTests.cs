using DwC_A.Exceptions;
using DwC_A.Meta;
using DwC_A.Terms;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class FieldMetadataTests
    {
        readonly ICollection<FieldType> fieldTypes = new List<FieldType>()
        {
            new FieldType()
            {
                Index = 1,
                Term = Terms.acceptedNameUsage
            },
            new FieldType()
            {
                Index = 2,
                Term = Terms.acceptedNameUsageID
            }
        };

        IFieldMetaData fieldMetaData;

        [Fact]
        public void ShouldThrowOnTermNotFound()
        {
            fieldMetaData = new FieldMetaData(null, fieldTypes);
            Assert.Throws<TermNotFoundException>(() => fieldMetaData[Terms.scientificName]);
        }

        [Fact]
        public void ShouldReturn2For_acceptedNameUsageID()
        {
            fieldMetaData = new FieldMetaData(null, fieldTypes);
            Assert.Equal(2, fieldMetaData.IndexOf(Terms.acceptedNameUsageID));
        }

        [Fact]
        public void ShouldReturnCount3WithId()
        {
            fieldMetaData = new FieldMetaData( new IdFieldType() { Index = 0 }, fieldTypes);
            Assert.Equal(3, fieldMetaData.Count());
        }
    }
}
