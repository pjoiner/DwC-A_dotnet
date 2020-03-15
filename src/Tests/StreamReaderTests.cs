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
            var data = "abc,\"def\nghi\",jkl\nmno,pqr";
            using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                using(var reader = new StreamReader(stream))
                {
                    var actual = new StringBuilder();
                    Assert.True(reader.ReadRow(fileMetaDataMock.Object, ref actual));
                    Assert.Equal("abc,\"def\nghi\",jkl", actual.Flush());
                    Assert.True(reader.ReadRow(fileMetaDataMock.Object, ref actual));
                    Assert.Equal("mno,pqr", actual.Flush());
                    Assert.False(reader.ReadRow(fileMetaDataMock.Object, ref actual));
                }
            }
        }
    }
}
