using DwC_A.Config;
using DwC_A.Meta;
using System;

namespace DwC_A.Factories
{
    public abstract class AbstractFactory : IAbstractFactory
    {
        private FactoryConfiguration configuration = new FactoryConfiguration();

        public AbstractFactory(Action<FactoryConfiguration> configFunc = null)
        {
            if(configFunc != null)
            {
                configFunc(configuration);
            }
        }

        public ArchiveReader CreateArchiveReader(string fileName)
        {
            return new ArchiveReader(fileName, this);
        }

        public virtual IArchiveFolder CreateArchiveFolder(string fileName, string outputPath)
        {
            return new ArchiveFolder(fileName, outputPath );
        }

        public virtual IMetaDataReader CreateMetaDataReader()
        {
            return new MetaDataReader();
        }

        public virtual ITokenizer CreateTokenizer(IFileMetaData fileMetaData)
        {
            return new Tokenizer(fileMetaData);
        }

        public virtual IRowFactory CreateRowFactory()
        {
            return new RowFactory();
        }

        public virtual IFileReaderAggregate CreateFileReader(string fileName, IFileMetaData fileMetaData)
        {
            return new FileReader(fileName, 
                CreateRowFactory(), 
                CreateTokenizer(fileMetaData), 
                fileMetaData,
                configuration.GetOptions<FileReaderConfiguration>());
        }

        public virtual IFileMetaData CreateCoreMetaData(CoreFileType coreFileType)
        {
            return new CoreFileMetaData(coreFileType);
        }

        public virtual IFileMetaData CreateExtensionMetaData(ExtensionFileType extensionFileType)
        {
            return new ExtensionFileMetaData(extensionFileType);
        }
    }
}
