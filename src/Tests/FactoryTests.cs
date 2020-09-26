using DwC_A;
using DwC_A.Config;
using DwC_A.Factories;
using DwC_A.Meta;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class FactoryTests
    {
        CoreFileType coreFileType = new CoreFileType()
        {
            FieldsEnclosedBy = "\"",
            FieldsTerminatedBy = "\\t",
            LinesTerminatedBy = "\\n"
        };

        public FactoryTests()
        {
            coreFileType.Files.Add("file1.txt");
        }
        
        [Fact]
        public void ShouldCreateTokenizer()
        {
            var factory = new DefaultFactory();
            var fileMetaData = new CoreFileMetaData(coreFileType);
            var tokenizer = factory.CreateTokenizer(fileMetaData);
            Assert.IsType<Tokenizer>(tokenizer);
            Assert.IsAssignableFrom<ITokenizer>(tokenizer);
        }

        [Fact]
        public void ShouldCreateFileReader()
        {
            var factory = new DefaultFactory(cfg =>
            {
                cfg.Add<FileReaderConfiguration>(cfg => cfg.BufferSize = 0);
            });
            var fileReader = factory.CreateFileReader("./resources/whales/whales.txt", new CoreFileMetaData(coreFileType));
            Assert.IsAssignableFrom<IFileReader>(fileReader);
            //Since the BufferSize is 0 this should throw when it tries to open the file
            Assert.Throws<ArgumentOutOfRangeException>(() => fileReader.DataRows.First());
        }
    }
}
