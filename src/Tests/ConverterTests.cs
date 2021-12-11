using DwC_A;
using DwC_A.Terms;
using DwC_A.Extensions;
using System.Linq;
using Xunit;
using System;
using DwC_A.Exceptions;
using Moq;
using System.ComponentModel;
using System.Globalization;

namespace Tests
{
    public class ConverterTests
    {
        private readonly ArchiveReader archive = new ArchiveReader("./resources/dwca-vascan-v37.5");
        private readonly IFileReader taxon;
        private readonly Mock<IRow> mockRow = new Mock<IRow>();

        [TypeConverter(typeof(EventDateConverter))]
        private class EventDate
        {
            public DateTime Begin { get; set; }
            public DateTime End { get; set; }
        }

        internal class EventDateConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) ? true : base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string valueStr)
                {
                    var dates = valueStr.Split('/'); 
                    return new EventDate()
                    {
                        Begin = dates.Length > 0 ? DateTime.Parse(dates[0]) : DateTime.MinValue,
                        End = dates.Length > 1 ? DateTime.Parse(dates[1]) : DateTime.MaxValue
                    };
                }
                return base.ConvertFrom(context, culture, value);
            }

            public override bool IsValid(ITypeDescriptorContext context, object value)
            {
                return value is string;
            }
        }

        public ConverterTests()
        {
            taxon = archive.CoreFile;
            var eventDate = "12-10-1990/12-17-1990";
            mockRow.Setup(n => n[Terms.eventDate]).Returns(eventDate);
            mockRow.Setup(n => n.TryGetField(Terms.eventDate, out eventDate)).Returns(true);
            mockRow.Setup(n => n[0]).Returns(eventDate);
        }

        [Fact]
        public void ShouldConvertIndividualCountToInt32()
        {
            var row = taxon.DataRows.FirstOrDefault();
            Assert.Equal(73, row.Convert<int>(Terms.acceptedNameUsageID));
            Assert.Equal(73, row.Convert(Terms.acceptedNameUsageID, typeof(int)));
            Assert.Equal(73, row.Convert<int>(0));
            Assert.Equal(73, row.ConvertNullable<int>(Terms.acceptedNameUsageID));
        }

        [Fact]
        public void ShouldConvertModifiedToDateTime()
        {
            var row = taxon.DataRows.FirstOrDefault();
            Assert.Equal(DateTime.Parse("2011-02-21T12:19-0500"), row.Convert<DateTime>(Terms.modified));
            Assert.Equal(DateTime.Parse("2011-02-21T12:19-0500"), row.Convert(Terms.modified, typeof(DateTime)));
            Assert.Equal(DateTime.Parse("2011-02-21T12:19-0500"), row.Convert<DateTime>(20));
            Assert.Equal(DateTime.Parse("2011-02-21T12:19-0500"), row.ConvertNullable<DateTime>(Terms.modified));
        }

        [Fact]
        public void ShouldThrowOnConvert()
        {
            var row = taxon.DataRows.FirstOrDefault();
            Assert.Throws<ArgumentException>(() => row.Convert<int>(Terms.scientificName));
            Assert.Throws<TermNotFoundException>(() => row.Convert<string>(Terms.sex));
            Assert.Throws<NotSupportedException>(() => row.Convert<int>(Terms.parentNameUsageID));

            Assert.Throws<ArgumentOutOfRangeException>(() => row.Convert<int>(999));

            Assert.Throws<ArgumentException>(() => row.Convert(Terms.scientificName, typeof(int)));
            Assert.Throws<TermNotFoundException>(() => row.Convert(Terms.sex, typeof(string)));
            Assert.Throws<NotSupportedException>(() => row.Convert(Terms.parentNameUsageID, typeof(int)));

            Assert.Throws<ArgumentOutOfRangeException>(() => row.Convert(999, typeof(int)));

            Assert.Throws<ArgumentException>(() => row.ConvertNullable<int>(Terms.scientificName));
            Assert.Throws<TermNotFoundException>(() => row.ConvertNullable<int>(Terms.sex));

            Assert.Throws<ArgumentOutOfRangeException>(() => row.ConvertNullable<int>(999));
        }

        [Fact]
        public void ShouldNotThrowOnNull()
        {
            var row = taxon.DataRows.FirstOrDefault();
            Assert.Null(row.ConvertNullable<int>(Terms.parentNameUsageID));
        }

        [Fact]
        public void TryConvertShouldReturnTrue()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvert<int>(Terms.acceptedNameUsageID, out int acceptedNameUsageID);
            Assert.True(result);
            Assert.Equal(ConvertResult.Success, result); 
            Assert.Equal(73, acceptedNameUsageID);
        }

        [Fact]
        public void TryConvertShouldReturnFalseOnTermNotFound()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvert<int>(Terms.individualCount, out int individualCount);
            Assert.False(result);
            Assert.Matches("Term .* not found", result.Message);
        }

        [Fact]
        public void TryConvertShouldReturnFalseOnNull()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvert<int>(Terms.parentNameUsageID, out int parentNameUsageID);
            Assert.False(result);
            Assert.Matches("Term .* with value \\(null\\) could not be converted to type System.Int32", result.Message);
        }

        [Fact]
        public void TryConvertShouldReturnFalseOnInvalidCast()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvert<int>(Terms.scientificName, out int scientificName);
            Assert.False(result);
            Assert.Matches("could not be converted to type System.Int32", result.Message);
        }

        [Fact]
        public void TryConvertShouldReturnFalseOnIndexOutOfRange()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvert<int>(999, out int scientificName);
            Assert.False(result);
            Assert.Matches("Index 999 out of range", result.Message);
        }

        [Fact]
        public void TryConvertNullableShouldReturnTrue()
        {
            var row = taxon.DataRows.FirstOrDefault();
            Assert.True(row.TryConvertNullable<int>(Terms.parentNameUsageID, out int? parentNameUsageID));
            Assert.Null(parentNameUsageID);
            Assert.True(row.TryConvertNullable<int>(Terms.acceptedNameUsageID, out int? acceptedNameUsageID));
            Assert.Equal(73, acceptedNameUsageID);
            Assert.True(row.TryConvertNullable<DateTime>(Terms.modified, out DateTime? modified));
            Assert.Equal(DateTime.Parse("2011-02-21T12:19-0500"), modified);
        }

        [Fact]
        public void TryConvertNullableShouldReturnFalse()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvertNullable<int>(Terms.scientificNameID, out int? scientificNameID);
            Assert.False(result);
            Assert.Matches("Term .* not found", result.Message);
        }

        [Fact]
        public void TryConvertNullableShouldReturnFalseOnInvalidCast()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvertNullable<int>(Terms.scientificName, out int? scientificName);
            Assert.False(result);
            Assert.Matches("could not be converted to type System.Int32", result.Message);
        }

        [Fact]
        public void TryConvertNullableShoulReturnFalseOnInvalidCast()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var result = row.TryConvertNullable<int>(5, out int? scientificName);
            Assert.False(result);
            Assert.Matches("Field at index 5 with value .* could not be converted to type System.Int32", result.Message);
        }

        public record MyType(string Name, int Value);
        [Fact]
        public void ShouldFailOnNoConverter()
        {
            var row = taxon.DataRows.FirstOrDefault();
            var actual = row.TryConvert<MyType>(Terms.acceptedNameUsage, out MyType value);
            Assert.False(actual);
        }

        [Fact]
        public void ShouldConvertEventDate()
        {
            var eventDate = mockRow.Object.Convert<EventDate>(Terms.eventDate);
            DateTime expected = new DateTime(1990, 12, 10);
            Assert.Equal(expected, eventDate.Begin);
        }

        [Fact]
        public void ShouldConvertEventDateUsingIndex()
        {
            var eventDate = mockRow.Object.Convert<EventDate>(0);
            DateTime expected = new DateTime(1990, 12, 10);
            Assert.Equal(expected, eventDate.Begin);
        }

        [Fact]
        public void ShouldReturnValueObjectOnConvertTermWithTypeOf()
        {
            var eventDate = mockRow.Object.Convert(Terms.eventDate, typeof(EventDate)) as EventDate;
            DateTime expected = new DateTime(1990, 12, 10);
            Assert.Equal(expected, eventDate.Begin);
        }

        [Fact]
        public void ShouldReturnValueObjectOnConvertIndexWithTypeOf()
        {
            var eventDate = mockRow.Object.Convert(0, typeof(EventDate)) as EventDate;
            DateTime expected = new DateTime(1990, 12, 10);
            Assert.Equal(expected, eventDate.Begin);
        }

        [Fact]
        public void ShouldConvertEventDateOnTry()
        {
            var result = mockRow.Object.TryConvert<EventDate>(Terms.eventDate, out EventDate eventDate);
            Assert.True(result);
            DateTime expected = new DateTime(1990, 12, 10);
            Assert.Equal(expected, eventDate.Begin);
        }
    }
}
