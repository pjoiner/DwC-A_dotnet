using Dwc.Text;
using DWC_A;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

        Mock<IFileAttributes> fileAttributesMock = new Mock<IFileAttributes>();

        ICollection<FieldType> fieldTypes = new FieldType[]
        {
            new FieldType(){ Index = "0", Term = "id" }
        };

        public FileReaderTests()
        {
            fileAttributesMock.Setup(n => n.FieldsEnclosedBy).Returns("");
            fileAttributesMock.Setup(n => n.FieldsTerminatedBy).Returns("\t");
            fileAttributesMock.Setup(n => n.LinesTerminatedBy).Returns("\n");
            fileAttributesMock.Setup(n => n.IgnoreHeaderLines).Returns("1");
            fileAttributesMock.Setup(n => n.Encoding).Returns("UTF-8");
            rowFactory = new RowFactory();
            indexFactory = new IndexFactory();
            tokenizer = new Tokenizer(fileAttributesMock.Object);
        }

        [Fact]
        public void ShouldEnumerateFile()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object, fieldTypes, indexFactory))
            {
                Assert.NotEmpty(fileReader.Rows.ToArray());
            }
        }

        [Fact]
        public void ShouldReturnHeaderRow()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object, fieldTypes, indexFactory))
            {
                Assert.Single(fileReader.HeaderRows);
            }
        }

        [Fact]
        public void ShouldReturnDataRows()
        {
            using (IFileReader fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object, fieldTypes, indexFactory))
            {
                Assert.NotEmpty(fileReader.DataRows);
            }
        }

        [Fact]
        public void ShouldSeekToBeginningForHeaderRows()
        {
            using (var fileReader = new FileReader(fileName, rowFactory, tokenizer, fileAttributesMock.Object, fieldTypes, indexFactory))
            {
                Assert.NotEmpty(fileReader.DataRows);
                Assert.NotEmpty(fileReader.HeaderRows);
            }
        }

        [Fact]
        public void ShouldCreateIndexOnResourceRelationship()
        {
            using (var fileReader = new FileReader(resourceRelationShipFileName, rowFactory, tokenizer, fileAttributesMock.Object, fieldTypes, indexFactory))
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
            using (var fileReader = new FileReader(resourceRelationShipFileName, rowFactory, tokenizer, fileAttributesMock.Object, fieldTypes, indexFactory))
            {
                var idIndex = fileReader.CreateIndexOn("id");
                var rowsWithId2850 = fileReader.ReadRowsAtIndex(idIndex, "2850");
                Assert.NotEmpty(rowsWithId2850);
            }
        }
    }
}
