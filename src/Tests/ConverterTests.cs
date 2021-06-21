using DwC_A;
using DwC_A.Terms;
using DwC_A.Extensions;
using System.Linq;
using Xunit;
using System;
using DwC_A.Exceptions;

namespace Tests
{
    public class ConverterTests
    {
        private readonly ArchiveReader archive = new ArchiveReader("./resources/dwca-vascan-v37.5");
        private readonly IFileReader taxon;

        public ConverterTests()
        {
            taxon = archive.CoreFile;
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
            Assert.Throws<FormatException>(() => row.Convert<int>(Terms.scientificName));
            Assert.Throws<TermNotFoundException>(() => row.Convert<string>(Terms.sex));
            Assert.Throws<InvalidCastException>(() => row.Convert<int>(Terms.parentNameUsageID));

            Assert.Throws<ArgumentOutOfRangeException>(() => row.Convert<int>(999));

            Assert.Throws<FormatException>(() => row.Convert(Terms.scientificName, typeof(int)));
            Assert.Throws<TermNotFoundException>(() => row.Convert(Terms.sex, typeof(string)));
            Assert.Throws<InvalidCastException>(() => row.Convert(Terms.parentNameUsageID, typeof(int)));

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
            Assert.True(row.TryConvert<int>(Terms.acceptedNameUsageID, out int acceptedNameUsageID));
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
    }
}
