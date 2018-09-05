﻿using DWC_A;
using Moq;
using System.Linq;
using System.Text;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Tests
{
    public class FileReaderTests
    {
        private const string fileName = "./resources/dwca-vascan-v37.5/taxon.txt";
        private const string resourceRelationShipFileName = "./resources/dwca-vascan-v37.5/resourcerelationship.txt";
        private readonly IRowFactory rowFactory;
        private readonly ITokenizer tokenizer;
        private readonly IIndexFactory indexFactory;

        Mock<IFileMetaData> fileMetaDataMock = new Mock<IFileMetaData>();

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
            indexFactory = new IndexFactory();
            tokenizer = new Tokenizer(fileMetaDataMock.Object);
        }

        [Fact]
        public void ShouldEnumerateFile()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileMetaDataMock.Object, indexFactory))
            {
                Assert.NotEmpty(fileReader.Rows.ToArray());
            }
        }

        [Fact]
        public void ShouldReturnHeaderRow()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileMetaDataMock.Object, indexFactory))
            {
                Assert.Single(fileReader.HeaderRows);
            }
        }

        [Fact]
        public void ShouldReturnDataRows()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileMetaDataMock.Object, indexFactory))
            {
                Assert.NotEmpty(fileReader.DataRows);
            }
        }

        [Fact]
        public void ShouldSeekToBeginningForHeaderRows()
        {
            using (var fileReader = new FileReader(fileName, rowFactory, tokenizer, fileMetaDataMock.Object, indexFactory))
            {
                Assert.NotEmpty(fileReader.DataRows);
                Assert.NotEmpty(fileReader.HeaderRows);
            }
        }

        [Fact]
        public void ShouldCreateIndexOnResourceRelationship()
        {
            using (var fileReader = new FileReader(resourceRelationShipFileName, rowFactory, tokenizer, fileMetaDataMock.Object, indexFactory))
            {
                var idIndex = fileReader.CreateIndexOn("id");
                Assert.NotEmpty(idIndex.OffsetsAt("2850"));
                Assert.Equal(429, idIndex.OffsetsAt("2850").FirstOrDefault());
                Assert.NotEmpty(idIndex.OffsetsAt("31391"));
            }
        }

        [Fact]
        public void ShouldReadAllRowsAtIndexValue()
        {
            using (var fileReader = new FileReader(resourceRelationShipFileName, rowFactory, tokenizer, fileMetaDataMock.Object, indexFactory))
            {
                var idIndex = fileReader.CreateIndexOn("id");
                var rowsWithId2850 = fileReader.ReadRowsAtIndex(idIndex, "2850");
                Assert.NotEmpty(rowsWithId2850);
            }
        }
    }
}
