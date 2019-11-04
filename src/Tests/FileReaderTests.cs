using DwC_A;
using DwC_A.Factories;
using DwC_A.Meta;
using Moq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Tests
{
    public class FileReaderTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";
        private const string resourceRelationShipFileName = "./resources/dwca-vascan-v37.5/resourcerelationship.txt";
        private readonly IRowFactory rowFactory;
        private readonly ITokenizer tokenizer;
        private readonly Mock<IFileMetaData> fileMetaDataMock = new Mock<IFileMetaData>();

        public FileReaderTests()
        {
            fileMetaDataMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileMetaDataMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileMetaDataMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            fileMetaDataMock.Setup(n => n.HeaderRowCount).Returns(1);
            fileMetaDataMock.Setup(n => n.Encoding).Returns(Encoding.UTF8);
            fileMetaDataMock.Setup(n => n.Fields.IndexOf("id")).Returns(0);
            fileMetaDataMock.Setup(n => n.LineTerminatorLength).Returns(1);
            rowFactory = new RowFactory();
            tokenizer = new Tokenizer(fileMetaDataMock.Object);
        }

        [Fact]
        public void ShouldEnumerateFile()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
                Assert.NotEmpty(fileReader.Rows.ToArray());
        }

        [Fact]
        public async Task ShouldEnumerateFileAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.NotEmpty(await fileReader.GetDataRowsAsync().ToArrayAsync());
        }

        [Fact]
        public void ShouldReturnHeaderRow()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.Single(fileReader.HeaderRows);
        }

        [Fact]
        public async Task ShouldReturnHeaderRowAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.Single(await fileReader.GetHeaderRowsAsync().ToArrayAsync());
        }

        [Fact]
        public void ShouldReturnDataRows()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.NotEmpty(fileReader.DataRows);
        }

        [Fact]
        public async Task ShouldReturnDataRowsAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.NotEmpty(await fileReader.GetDataRowsAsync().ToArrayAsync());
        }

        [Fact]
        public void ShouldSeekToBeginningForHeaderRows()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.NotEmpty(fileReader.DataRows);
            Assert.NotEmpty(fileReader.HeaderRows);
        }

        [Fact]
        public async Task ShouldSeekToBeginningForHeaderRowsAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object);
            Assert.NotEmpty(await fileReader.GetDataRowsAsync().ToArrayAsync());
            Assert.NotEmpty(await fileReader.GetHeaderRowsAsync().ToArrayAsync());
        }
    }
}
