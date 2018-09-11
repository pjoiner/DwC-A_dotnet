using DWC_A;
using DWC_A.Meta;
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

        IRow row;

        public RowTests()
        {
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("Name")).Returns(0);
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("Value")).Returns(1);
            row = new Row(fields, fileMetaDataMock.Object);
        }

        [Fact]
        public void ShowReturnField()
        {
            Assert.Equal("nameField", row[0]);
            Assert.Equal("valueField", row["Value"]);
            Assert.Equal(2, row.Fields.Count());
        }
    }
}
