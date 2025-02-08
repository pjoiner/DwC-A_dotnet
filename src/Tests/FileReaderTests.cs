using DwC_A;
using DwC_A.Config;
using DwC_A.Factories;
using DwC_A.Meta;
using Moq;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Tests
{
    public class FileReaderTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";
        private readonly IRowFactory rowFactory;
        private readonly ITokenizer tokenizer;
        private readonly Mock<IFileMetaData> fileMetaDataMock = new();

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
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
                Assert.NotEmpty(fileReader.Rows.ToArray());
        }

        [Fact]
        public async Task ShouldEnumerateFileAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.NotEmpty(await fileReader
                .GetDataRowsAsync(TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
        }

        [Fact]
        public void ShouldReturnHeaderRow()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.Single(fileReader.HeaderRows);
        }

        [Fact]
        public async Task ShouldReturnHeaderRowAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.Single(await fileReader
                .GetHeaderRowsAsync(TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
        }

        [Fact]
        public void ShouldReturnDataRows()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.NotEmpty(fileReader.DataRows);
        }

        [Fact]
        public async Task ShouldReturnDataRowsAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.NotEmpty(await fileReader
                .GetDataRowsAsync(TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ShouldThrowOnCancellation()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            var ct = new CancellationToken(true);
            await Assert.ThrowsAsync<OperationCanceledException>(async () => 
            { 
                await fileReader
                .GetDataRowsAsync(ct)
                .ToArrayAsync(TestContext.Current.CancellationToken); 
            });
        }

        [Fact]
        public void ShouldSeekToBeginningForHeaderRows()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.NotEmpty(fileReader.DataRows);
            Assert.NotEmpty(fileReader.HeaderRows);
        }

        [Fact]
        public async Task ShouldSeekToBeginningForHeaderRowsAsync()
        {
            fileMetaDataMock.Setup(n => n.FileName).Returns(fileName);
            var fileReader = new FileReader(fileName,
                rowFactory, tokenizer, fileMetaDataMock.Object,
                new FileReaderConfiguration());
            Assert.NotEmpty(await fileReader
                .GetDataRowsAsync(TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
            Assert.NotEmpty(await fileReader
                .GetHeaderRowsAsync(TestContext.Current.CancellationToken)
                .ToArrayAsync(TestContext.Current.CancellationToken));
        }
    }
}
