using DwC_A.Extensions;
using DwC_A.Meta;
using Moq;
using System.IO;
using System.Text;
using Xunit;

namespace Tests
{
    public class StreamReaderTests
    {
        string data = "abc,\"def\nghi\",jkl\nmno,pqr";
        Mock<IFileMetaData> fileMetaDataMock = new Mock<IFileMetaData>();

        public StreamReaderTests()
        {
            fileMetaDataMock.Setup(n => n.FieldsEnclosedBy).Returns("\"");
            fileMetaDataMock.Setup(n => n.FieldsTerminatedBy).Returns(",");
            fileMetaDataMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            fileMetaDataMock.Setup(n => n.Encoding).Returns(Encoding.UTF8);
        }

        [Fact]
        public void ShouldReadStreamWithQuotes()
        {
            using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                using(var reader = new StreamReader(stream))
                {
                    var actual = new StringBuilder();
                    Assert.Equal("abc,\"def\nghi\",jkl", reader.ReadRow(fileMetaDataMock.Object));
                    Assert.Equal("mno,pqr", reader.ReadRow(fileMetaDataMock.Object));
                    Assert.Null(reader.ReadRow(fileMetaDataMock.Object));
                }
            }
        }

        [Fact]
        public async void ShouldReadStreamWithQuotesAsync()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                using (var reader = new StreamReader(stream))
                {
                    Assert.Equal("abc,\"def\nghi\",jkl", await reader.ReadRowAsync(fileMetaDataMock.Object));
                    Assert.Equal("mno,pqr", await reader.ReadRowAsync(fileMetaDataMock.Object));
                    Assert.Null(await reader.ReadRowAsync(fileMetaDataMock.Object));
                }
            }
        }
    }
}
