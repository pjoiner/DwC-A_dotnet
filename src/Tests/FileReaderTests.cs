using Dwc.Text;
using DWC_A;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Tests
{
    public class FileReaderTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";
        private readonly IRowFactory rowFactory;
        private readonly ITokenizer tokenizer;

        Mock<IFileAttributes> fileAttributesMock = new Mock<IFileAttributes>();

        ICollection<FieldType> fieldTypes = new FieldType[]
        {

        };

        public FileReaderTests()
        {
            fileAttributesMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileAttributesMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileAttributesMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            fileAttributesMock.Setup(n => n.IgnoreHeaderLines).Returns("1");
            rowFactory = new RowFactory(fieldTypes);
            tokenizer = new Tokenizer(fileAttributesMock.Object);
        }

        [Fact]
        public void ShouldEnumerateFile()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object))
            {
                Assert.NotEmpty(fileReader.Rows.ToArray());
            }
        }

        [Fact]
        public void ShouldReturnHeaderRow()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object))
            {
                Assert.Single(fileReader.HeaderRows);
            }
        }

        [Fact]
        public void ShouldReturnDataRows()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object))
            {
                Assert.NotEmpty(fileReader.DataRows);
            }
        }

    }
}
