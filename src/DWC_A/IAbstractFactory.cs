using Dwc.Text;
using DWC_A.Meta;
using System.Collections.Generic;

namespace DWC_A
{
    public interface IAbstractFactory
    {
        IArchiveFolder CreateArchiveFolder(string fileName, string outputPath);
        IMetaDataReader CreateMetaDataReader();
        ITokenizer CreateTokenizer(IFileAttributes fileAttributes);
        IRowFactory CreateRowFactory(ICollection<FieldType> fieldTypes);
        IFileReader CreateFileReader(string fileName,
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileAttributes fileAttributes);
    }
}
