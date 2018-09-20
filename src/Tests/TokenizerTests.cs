using DwC_A;
using DwC_A.Meta;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class TokenizerTests
    {
        private string csvLine = "abc,def,ghi";
        private string csvLineWithTrailingComma = "abc,def,";
        private string csvWithQuotes = "abc,\"def,ghi\"";

        private Mock<IFileMetaData> fileMetaDataMockWithQuotes = new Mock<IFileMetaData>();
        private Mock<IFileMetaData> fileMetaDataMockWithoutQuotes = new Mock<IFileMetaData>();

        public TokenizerTests()
        {
            fileMetaDataMockWithQuotes.Setup(n => n.FieldsEnclosedBy).Returns("\"");
            fileMetaDataMockWithQuotes.Setup(n => n.FieldsTerminatedBy).Returns(",");
            fileMetaDataMockWithQuotes.Setup(n => n.LinesTerminatedBy).Returns("\n");

            fileMetaDataMockWithoutQuotes.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileMetaDataMockWithoutQuotes.Setup(n => n.FieldsTerminatedBy).Returns(",");
            fileMetaDataMockWithoutQuotes.Setup(n => n.LinesTerminatedBy).Returns("\n");
        }

        [Fact]
        public void ParseCSVLineShouldReturnThreeFields()
        {
            Tokenizer tokenizer = new Tokenizer(fileMetaDataMockWithoutQuotes.Object);
            var fields = tokenizer.Split(csvLine);
            Assert.Equal(3, fields.ToList().Count);
        }

        [Fact]
        public void FirstCSVFieldShouldBeabc()
        {
            Tokenizer tokenizer = new Tokenizer(fileMetaDataMockWithoutQuotes.Object);
            var fields = tokenizer.Split(csvLine);
            Assert.Equal("abc", fields.ToArray()[0]);            
        }

        [Fact]
        public void SecondCSVFieldShouldBedef()
        {
            Tokenizer tokenizer = new Tokenizer(fileMetaDataMockWithoutQuotes.Object);
            var fields = tokenizer.Split(csvLine);
            Assert.Equal("def", fields.ToArray()[1]);
        }

        [Fact]
        public void ThirdCSVFieldShouldBeghi()
        {
            Tokenizer tokenizer = new Tokenizer(fileMetaDataMockWithoutQuotes.Object);
            var fields = tokenizer.Split(csvLine);
            Assert.Equal("ghi", fields.ToArray()[2]);
        }

        [Fact]
        public void CSVFieldWithTrailineCommaShouldProduceEmptyField()
        {
            Tokenizer tokenizer = new Tokenizer(fileMetaDataMockWithoutQuotes.Object);
            var fields = tokenizer.Split(csvLineWithTrailingComma);
            Assert.Equal("", fields.Last());
        }

        [Fact]
        public void CSVFieldWithQuotesShouldReturndefCommaghi()
        {
            Tokenizer tokenizer = new Tokenizer(fileMetaDataMockWithQuotes.Object);
            var fields = tokenizer.Split(csvWithQuotes);
            Assert.Equal("def,ghi", fields.Last());
        }

        [Fact]
        public void ShouldThrowOnNullFileAttributes()
        {
            Assert.Throws<ArgumentNullException>(() => new Tokenizer(null));
        }

        [Fact]
        public void DefaultFileAttributesShouldNotThrow()
        {
            var tokenizer = new Tokenizer(new CoreFileMetaData(null));
            var fields = tokenizer.Split(csvWithQuotes);
            Assert.Equal("def,ghi", fields.Last());
        }

    }
}
