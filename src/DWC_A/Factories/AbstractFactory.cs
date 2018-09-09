using Dwc.Text;
using DWC_A.Meta;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DWC_A.Factories
{
    public abstract class AbstractFactory : IAbstractFactory
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger logger;

        public AbstractFactory(ILoggerFactory loggerFactory = null)
        {
            this.loggerFactory = loggerFactory == null ? NullLoggerFactory.Instance : loggerFactory;
            logger = this.loggerFactory.CreateLogger(this.GetType().Name);
            logger.LogDebug($"{this.GetType().Name} factory created");
        }

        public virtual IArchiveFolder CreateArchiveFolder(string fileName, string outputPath)
        {
            return new ArchiveFolder(loggerFactory.CreateLogger<ArchiveFolder>(), 
                fileName, outputPath );
        }

        public virtual IMetaDataReader CreateMetaDataReader()
        {
            return new MetaDataReader(loggerFactory.CreateLogger<MetaDataReader>());
        }

        public virtual ITokenizer CreateTokenizer(IFileMetaData fileMetaData)
        {
            this.logger.LogDebug($"Creating Tokenizer for {fileMetaData.FileName} " +
                $"using {fileMetaData.GetType().Name}");
            return new Tokenizer(fileMetaData);
        }

        public virtual IRowFactory CreateRowFactory()
        {
            return new RowFactory();
        }

        public virtual IFileReader CreateFileReader(string fileName, IFileMetaData fileMetaData)
        {
            return new FileReader(loggerFactory.CreateLogger<FileReader>(),
                fileName, 
                CreateRowFactory(), 
                CreateTokenizer(fileMetaData), 
                fileMetaData, 
                CreateIndexFactory());
        }

        public virtual IIndexFactory CreateIndexFactory()
        {
            return new IndexFactory();
        }

        public virtual IFileMetaData CreateCoreMetaData(CoreFileType coreFileType)
        {
            return new CoreFileMetaData(loggerFactory.CreateLogger<CoreFileMetaData>(), 
                coreFileType);
        }

        public virtual IFileMetaData CreateExtensionMetaData(ExtensionFileType extensionFileType)
        {
            return new ExtensionFileMetaData(loggerFactory.CreateLogger<ExtensionFileMetaData>(),
                extensionFileType);
        }
    }
}
