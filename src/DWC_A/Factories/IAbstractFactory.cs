using Dwc.Text;
using DWC_A.Meta;

namespace DWC_A.Factories
{
    public interface IAbstractFactory
    {
        IArchiveFolder CreateArchiveFolder(string fileName, string outputPath);
        IMetaDataReader CreateMetaDataReader();
        ITokenizer CreateTokenizer(IFileMetaData fileAttributes);
        IRowFactory CreateRowFactory();
        IFileReader CreateFileReader(string fileName,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData);
        IIndexFactory CreateIndexFactory();
        IFileMetaData CreateCoreMetaData(CoreFileType coreFileType);
        IFileMetaData CreateExtensionMetaData(ExtensionFileType extensionFileType);
    }
}
