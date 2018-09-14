using DWC_A.Factories;
using DWC_A.Meta;
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
        public void ShouldLogCreationOfNewTokenizer()
        {
            var factory = new DefaultFactory();
            var fileMetaData = new CoreFileMetaData(coreFileType);
            var tokenizer = factory.CreateTokenizer(fileMetaData);
            
        }
    }
}
