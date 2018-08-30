using DWC_A;
using DWC_A.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class RowTests
    {
        private IDictionary<string, int> fieldTypeIndex = new Dictionary<string, int>()
        {
            {"Name", 0 },
            {"Value", 1 }
        };

        private IEnumerable<string> fields = new string[]
        {
                "nameField", "valueField"
        };

        [Fact]
        public void ShowReturnField()
        {
            var row = new Row(fields, fieldTypeIndex);
            Assert.Equal("nameField", row[0]);
            Assert.Equal("valueField", row["Value"]);
        }

        [Fact]
        public void ShouldThrowOnTermNotFound()
        {
            var row = new Row(fields, fieldTypeIndex);
            Assert.Throws<TermNotFoundException>(() => row["notAField"]);
        }
    }
}
