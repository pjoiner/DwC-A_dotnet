using Dwc.Text;
using DwC_A.Meta;
using DWC_A;

namespace DwC_A
{
    public abstract class AbstractFactory : IFactory
    {
        public virtual MetaDataReader CreateMetaDataReader()
        {
            return new MetaDataReader();
        }

        public virtual ITokenizer CreateTokenizer(IFileAttributes fileAttributes)
        {
            return new Tokenizer(fileAttributes);
        }
    }
}
