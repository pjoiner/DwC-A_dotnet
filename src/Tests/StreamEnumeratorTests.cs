using DwC_A;
using DwC_A.Factories;
using DwC_A.Meta;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    //TODO:  use a MemoryStream for testing here to allow parallelism in tests
    public class StreamEnumeratorTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";
        private readonly IRowFactory rowFactory;
        private readonly ITokenizer tokenizer;

        readonly Mock<IFileMetaData> fileMetaDataMock = new();
        readonly Mock<IFileAttributes> fileAttributesMock = new();

        public StreamEnumeratorTests()
        {
            fileMetaDataMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileMetaDataMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileMetaDataMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            fileMetaDataMock.Setup(n => n.Encoding).Returns(Encoding.UTF8);
            fileMetaDataMock.Setup(n => n.Fields.Length).Returns(24);

            fileAttributesMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileAttributesMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileAttributesMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            fileAttributesMock.Setup(n => n.Encoding).Returns("UTF-8");

            rowFactory = new RowFactory();
            tokenizer = new Tokenizer(fileMetaDataMock.Object);
        }

        [Fact]
        public void ShouldEnumerateFile()
        {
            using Stream stream = new FileStream(fileName, FileMode.Open);
            var streamEnumerator = new DwC_A.StreamReader(
                rowFactory,
                tokenizer,
                fileMetaDataMock.Object);
            Assert.NotEmpty(streamEnumerator.ReadRows(stream).ToArray());
        }

        [Fact]
        public async Task ShouldEnumerateFileAsync()
        {
            using Stream stream = new FileStream(fileName, FileMode.Open);
            var streamEnumerator = new DwC_A.StreamReader(
                rowFactory,
                tokenizer,
                fileMetaDataMock.Object);
            Assert.NotEmpty(await streamEnumerator
                .ReadRowsAsync(stream, TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
        }

    }
}
