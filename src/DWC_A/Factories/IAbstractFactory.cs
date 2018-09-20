using DwC_A.Meta;

namespace DwC_A.Factories
{
    public interface IAbstractFactory
    {
        ArchiveReader CreateArchiveReader(string fileName, string outputPath);
        IArchiveFolder CreateArchiveFolder(string fileName, string outputPath);
        IMetaDataReader CreateMetaDataReader();
        ITokenizer CreateTokenizer(IFileMetaData fileAttributes);
        IRowFactory CreateRowFactory();
        IFileReader CreateFileReader(string fileName,
            IFileMetaData fileMetaData);
        IIndexFactory CreateIndexFactory();
        IFileMetaData CreateCoreMetaData(CoreFileType coreFileType);
        IFileMetaData CreateExtensionMetaData(ExtensionFileType extensionFileType);
    }
}
