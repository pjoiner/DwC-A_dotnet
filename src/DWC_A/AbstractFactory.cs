using Dwc.Text;
using DWC_A.Meta;
using System.Collections.Generic;

namespace DWC_A
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

        public virtual ITokenizer CreateTokenizer(IFileAttributes fileAttributes)
        {
            return new Tokenizer(fileAttributes);
        }

        public virtual IRowFactory CreateRowFactory()
        {
            return new RowFactory();
        }

        public IFileReader CreateFileReader(string fileName, IRowFactory rowFactory, ITokenizer tokenizer, IFileAttributes fileAttributes, ICollection<FieldType> fieldTypes)
        {
            return new FileReader(fileName, rowFactory, tokenizer, fileAttributes, fieldTypes);
        }
    }
}
