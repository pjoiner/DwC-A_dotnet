﻿using DWC_A.Factories;
using DWC_A.Meta;
using DWC_A.Terms;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class FileMetaDataTests
    {
        /* MetaDataReader test
        [Fact]
        public void ArchiveShouldContainCoreFile()
        {
            var metaDataReader = factory.CreateMetaDataReader();
            var archive = metaDataReader.ReadMetaData("./resources/dwca-vascan-v37.5");
            Assert.NotNull(archive);
        }
        */

        public static IEnumerable<object[]> GetMetaData()
        {
            IAbstractFactory factory = new DefaultFactory();
            yield return new object[] { factory.CreateCoreMetaData(null) };
            yield return new object[] { factory.CreateExtensionMetaData(null) };
        }

        [Theory]
        [MemberData(nameof(GetMetaData))]
        public void ShouldReturnDefaultMetaDataOnNull(IFileMetaData fileMetaData)
        {
            Assert.Null(fileMetaData.FileName);
            Assert.Null(fileMetaData.Fields);
            Assert.Equal(Encoding.UTF8, fileMetaData.Encoding);
            Assert.Equal("YYYY-MM-DD", fileMetaData.DateFormat);
            Assert.Equal("\"", fileMetaData.FieldsEnclosedBy);
            Assert.Equal(",", fileMetaData.FieldsTerminatedBy);
            Assert.Equal(0, fileMetaData.HeaderRowCount);
            Assert.Equal("\n", fileMetaData.LinesTerminatedBy);
            Assert.Equal(1, fileMetaData.LineTerminatorLength);
            Assert.Equal(RowTypes.SimpleDarwinRecord, fileMetaData.RowType);
        }
    }
}