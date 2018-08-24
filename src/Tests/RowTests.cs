using Dwc.Text;
using DwC_A.Exceptions;
using DWC_A;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class RowTests
    {
        private ICollection<FieldType> fieldTypes = new List<FieldType>()
        {
            new FieldType(){ Index = "0", Term="Name" },
            new FieldType(){ Index = "1", Term="Value"}
        };

        private IEnumerable<string> fields = new string[]
        {
                "nameField", "valueField"
        };

        [Fact]
        public void ShowReturnField()
        {
            var row = new Row(fields, fieldTypes);
            Assert.Equal("nameField", row[0]);
            Assert.Equal("valueField", row["Value"]);
        }

        [Fact]
        public void ShouldThrowOnTermNotFound()
        {
            var row = new Row(fields, fieldTypes);
            Assert.Throws<TermNotFoundException>(() => row["notAField"]);
        }
    }
}
