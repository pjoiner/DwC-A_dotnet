using DwC_A;
using DwC_A.Extensions;
using DwC_A.Terms;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class ConvertResultTests
    {
        private readonly Mock<IRow> mockRow = new Mock<IRow>();

        public ConvertResultTests()
        {
            var individualCount = "X";
            var eventDate = "2021-12-11";
            string indentificationID = default;
            mockRow.Setup(n => n[Terms.individualCount]).Returns(individualCount);
            mockRow.Setup(n => n[Terms.eventDate]).Returns(eventDate);
            mockRow.Setup(n => n.TryGetField(Terms.individualCount, out individualCount)).Returns(true);
            mockRow.Setup(n => n.TryGetField(Terms.identificationID, out indentificationID)).Returns(false);
            mockRow.Setup(n => n.TryGetField(Terms.eventDate, out eventDate)).Returns(true);
            mockRow.Setup(n => n[0]).Returns(individualCount);
        }

        [Fact]
        public void ShouldCompareTwoResultsSame()
        {
            var result1 = mockRow.Object.TryConvert<int>(Terms.individualCount, out int individualCount1);
            var result2 = mockRow.Object.TryConvert<int>(Terms.individualCount, out int individualCount2);
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void ShouldFailOnCompareTwoResultsDifferent()
        {
            var result1 = mockRow.Object.TryConvert<int>(Terms.individualCount, out int individualCount1);
            var result2 = mockRow.Object.TryConvert<int>(Terms.identificationID, out int indentificationID);
            Assert.NotEqual(result1, result2);
        }

        [Fact]
        public void ShouldCompareSuccess()
        {
            var result1 = mockRow.Object.TryConvert<DateTime>(Terms.eventDate, out DateTime value1);
            var result2 = mockRow.Object.TryConvert<DateTime>(Terms.eventDate, out DateTime value2);
            Assert.Equal(result1, result2);
        }
    }
}
