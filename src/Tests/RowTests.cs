using DwC_A;
using DwC_A.Meta;
using DwC_A.Terms;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class RowTests
    {
        Mock<IFileMetaData> fileMetaDataMock = new Mock<IFileMetaData>();

        IEnumerable<string> fields = new string[]
        {
                "nameField", "valueField"
        };

        FieldType fieldType = new FieldType()
        {
            Index = 1,
            IndexSpecified = true,
            Term = Terms.acceptedNameUsage
        };

        FieldType defaultFieldType = new FieldType()
        {
            IndexSpecified = false,
            Term = Terms.acceptedNameUsageID,
            Default = "Default Value"
        };

        IRow row;

        public RowTests()
        {
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("Name")).Returns(0);
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("Value")).Returns(1);
            fileMetaDataMock.Setup(n => n.Fields[It.IsAny<int>()]).Returns(fieldType);
            var valueFieldType = new FieldType()
            {
                Index = 1,
                IndexSpecified = true,
                Term = "Value"
            };
            fileMetaDataMock.Setup(n => n.Fields.TryGetFieldType("Value", out valueFieldType))
                .Returns(true);
            row = new Row(fields, fileMetaDataMock.Object.Fields);
        }

        [Fact]
        public void ShowReturnField()
        {
            Assert.Equal("nameField", row[0]);
            Assert.Equal("valueField", row["Value"]);
            Assert.Equal(2, row.Fields.Count());
        }

        [Fact]
        public void ShouldDisplayFieldMetaData()
        {
            Assert.Equal(Terms.acceptedNameUsage, row.FieldMetaData[1].Term);
        }

        [Fact]
        public void ShouldReturnDefaultValue()
        {
            var fieldMetaData = new FieldMetaData(null, new[] { fieldType, defaultFieldType });
            IRow row = new Row(fields, fieldMetaData);
            Assert.Equal("Default Value", row[Terms.acceptedNameUsageID]);
            Assert.True(row.TryGetField(Terms.acceptedNameUsageID, out string actual));
            Assert.Equal("Default Value", actual);
        }
    }
}
