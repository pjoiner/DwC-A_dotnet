using DWC_A;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class TokenizerTests
    {
        private string csvLine = "abc,def,ghi";
        private string csvLineWithTrailingComma = "abc,def,";
        private string csvWithQuotes = "abc,\"def,ghi\"";

        public TokenizerTests()
        {
        }

        [Fact]
        public void ParseCSVLineShouldReturnThreeFields()
        {
            Tokenizer tokenizer = new Tokenizer();
            var fields = tokenizer.Split(csvLine);
            Assert.Equal(3, fields.ToList().Count);
        }

        [Fact]
        public void FirstCSVFieldShouldBeabc()
        {
            Tokenizer tokenizer = new Tokenizer();
            var fields = tokenizer.Split(csvLine);
            Assert.Equal("abc", fields.ToArray()[0]);            
        }

        [Fact]
        public void SecondCSVFieldShouldBedef()
        {
            Tokenizer tokenizer = new Tokenizer();
            var fields = tokenizer.Split(csvLine);
            Assert.Equal("def", fields.ToArray()[1]);
        }

        [Fact]
        public void ThirdCSVFieldShouldBeghi()
        {
            Tokenizer tokenizer = new Tokenizer();
            var fields = tokenizer.Split(csvLine);
            Assert.Equal("ghi", fields.ToArray()[2]);
        }

        [Fact]
        public void CSVFieldWithTrailineCommaShouldProduceEmptyField()
        {
            Tokenizer tokenizer = new Tokenizer();
            var fields = tokenizer.Split(csvLineWithTrailingComma);
            Assert.Equal("", fields.Last());
        }

        [Fact]
        public void CSVFieldWithQuotesShouldReturndefCommaghi()
        {
            Tokenizer tokenizer = new Tokenizer(true);
            var fields = tokenizer.Split(csvWithQuotes);
            Assert.Equal("def,ghi", fields.Last());
        }

    }
}
