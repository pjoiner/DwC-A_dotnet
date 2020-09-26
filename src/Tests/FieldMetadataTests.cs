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
                IndexSpecified = true,
                Term = Terms.acceptedNameUsage
            },
            new FieldType()
            {
                Index = 2,
                IndexSpecified = true,
                Term = Terms.acceptedNameUsageID
            }
        };

        IFieldMetaData fieldMetaData;

        [Fact]
        public void ShouldReturnNegativeOneOnNotFound()
        {
            fieldMetaData = new FieldMetaData(null, fieldTypes);
            var actual = fieldMetaData.IndexOf(Terms.scientificName);
            Assert.Equal(-1, actual);
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
            var idField = new IdFieldType()
            {
                Index = 0,
                IndexSpecified = true
            };
            fieldMetaData = new FieldMetaData( idField, fieldTypes);
            Assert.Equal(3, fieldMetaData.Count());
        }

        [Fact]
        //issue#12
        public void ShouldNotCreateDuplicateIdFields()
        {
            var idField = new IdFieldType()
            {
                Index = 1,
                IndexSpecified = true
            };
            fieldMetaData = new FieldMetaData(idField, fieldTypes);
            Assert.Equal(2, fieldMetaData.Count());
        }
    }
}
