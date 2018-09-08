using Dwc.Text;
using DWC_A.Exceptions;
using DWC_A.Meta;
using DWC_A.Terms;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class FieldMetadataTests
    {
        readonly ICollection<FieldType> fieldTypes = new List<FieldType>()
        {
            new FieldType()
            {
                Index = "0",
                Term = Terms.acceptedNameUsage
            },
            new FieldType()
            {
                Index = "1",
                Term = Terms.acceptedNameUsageID
            }
        };

        [Fact]
        public void ShouldThrowOnTermNotFound()
        {
            var fieldMetaData = new FieldMetaData(null, fieldTypes);
            Assert.Throws<TermNotFoundException>(() => fieldMetaData[Terms.scientificName]);
        }

    }
}
