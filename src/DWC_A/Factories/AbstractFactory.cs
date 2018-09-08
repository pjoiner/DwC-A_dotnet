using Dwc.Text;
using DWC_A.Meta;

namespace DWC_A.Factories
{
    public abstract class AbstractFactory : IAbstractFactory
    {
        public virtual IArchiveFolder CreateArchiveFolder(string fileName, string outputPath)
        {
            return new ArchiveFolder(fileName, outputPath);
        }

        public virtual IMetaDataReader CreateMetaDataReader()
        {
            return new MetaDataReader();
        }

        public virtual ITokenizer CreateTokenizer(IFileMetaData fileAttributes)
        {
            return new Tokenizer(fileAttributes);
        }

        public virtual IRowFactory CreateRowFactory()
        {
            return new RowFactory();
        }

        public virtual IFileReader CreateFileReader(string fileName, IFileMetaData fileMetaData)
        {
            return new FileReader(fileName, 
                CreateRowFactory(), CreateTokenizer(fileMetaData), 
                fileMetaData, CreateIndexFactory());
        }

        public virtual IIndexFactory CreateIndexFactory()
        {
            return new IndexFactory();
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
