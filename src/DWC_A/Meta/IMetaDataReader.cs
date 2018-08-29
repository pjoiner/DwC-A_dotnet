namespace DWC_A.Meta
{
    public interface IMetaDataReader
    {
        Dwc.Text.Archive ReadMetaData(string path);
    }
}