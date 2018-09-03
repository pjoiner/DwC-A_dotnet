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

        public virtual IFileReader CreateFileReader(string fileName, ITokenizer tokenizer, IFileAttributes fileAttributes, ICollection<FieldType> fieldTypes)
        {
            return new FileReader(fileName, CreateRowFactory(), tokenizer, fileAttributes, fieldTypes, CreateIndexFactory());
        }

        public virtual IIndexFactory CreateIndexFactory()
        {
            return new IndexFactory();
        }
    }
}
