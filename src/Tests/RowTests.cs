using DWC_A;
using DWC_A.Exceptions;
using DWC_A.Meta;
using DWC_A.Terms;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class RowTests
    {
        private Mock<IFileMetaData> fileMetaDataMock = new Mock<IFileMetaData>();

        private IEnumerable<string> fields = new string[]
        {
                "nameField", "valueField"
        };

        public RowTests()
        {
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("Name")).Returns(0);
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("Value")).Returns(1);
        }
        [Fact]
        public void ShowReturnField()
        {
            var row = new Row(fields, fileMetaDataMock.Object);
            Assert.Equal("nameField", row[0]);
            Assert.Equal("valueField", row["Value"]);
        }

    }
}
