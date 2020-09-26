using DwC_A.Terms;
using Xunit;

namespace Tests
{
    public class TermsTests
    {
        [Fact]
        public void ShouldReturnTermShortName()
        {
            Assert.Equal("Occurrence", Terms.ShortName(Terms.Occurrence));
        }

        [Fact]
        public void ShouldReturnInputOnNotFound()
        {
            string occurrence = "occurrence";
            Assert.Equal(occurrence, Terms.ShortName(occurrence));
        }

        [Fact]
        public void ShouldNotThrowOnNull()
        {
            Assert.Null(Terms.ShortName(null));
        }
    }
}
