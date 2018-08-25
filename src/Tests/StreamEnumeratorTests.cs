using Dwc.Text;
using DWC_A;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tests
{
    //TODO:  use a MemoryStream for testing here to allow parallelism in tests
    public class StreamEnumeratorTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";
        private readonly IRowFactory rowFactory;
        private readonly ITokenizer tokenizer;

        Mock<IFileAttributes> fileAttributesMock = new Mock<IFileAttributes>();

        public StreamEnumeratorTests()
        {
            fileAttributesMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileAttributesMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileAttributesMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            rowFactory = new RowFactory(fieldTypes);
            tokenizer = new Tokenizer(fileAttributesMock.Object);
        }

        ICollection<FieldType> fieldTypes = new FieldType[]
        {

        };

        [Fact]
        public void ShouldEnumerateFile()
        {
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                var streamEnumerator = new StreamEnumerator(stream,
                    rowFactory,
                    tokenizer);
                Assert.NotEmpty(streamEnumerator.Rows.ToArray());
            }
        }

        [Fact]
        public void ShouldReturnHeaderRow()
        {
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                var streamEnumerator = new StreamEnumerator(stream,
                    rowFactory,
                    tokenizer);
                Assert.Single(streamEnumerator.HeaderRows(1));
            }
        }

        [Fact]
        public void ShouldReturnDataRows()
        {
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                var streamEnumerator = new StreamEnumerator(stream,
                    rowFactory,
                    tokenizer);
                Assert.NotEmpty(streamEnumerator.DataRows(1));
            }
        }
    }
}
