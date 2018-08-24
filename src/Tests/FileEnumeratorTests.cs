using Dwc.Text;
using DWC_A;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class FileEnumeratorTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";

        Mock<IFileAttributes> fileAttributesMock = new Mock<IFileAttributes>();

        public FileEnumeratorTests()
        {
            fileAttributesMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileAttributesMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileAttributesMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
        }

        ICollection<FieldType> fieldTypes = new FieldType[]
        {

        };

        [Fact]
        public void ShouldEnumerateFile()
        {
            IRowFactory factory = new RowFactory(fieldTypes);
            ITokenizer tokenizer = new Tokenizer(fileAttributesMock.Object);
            var fileEnumerator = new FileEnumerator(fileName, 
                factory,
                tokenizer);
            Assert.NotEmpty(fileEnumerator.Rows.ToArray());
        }
    }
}
