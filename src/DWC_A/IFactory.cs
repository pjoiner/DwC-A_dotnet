using Dwc.Text;
using DwC_A.Meta;
using DWC_A;

namespace DwC_A
{
    public interface IFactory
    {
        MetaDataReader CreateMetaDataReader();
        ITokenizer CreateTokenizer(IFileAttributes fileAttributes);
    }
}
