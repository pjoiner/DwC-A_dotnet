using System.IO;
using System.Xml.Serialization;

namespace DWC_A.Meta
{
    public class MetaDataReader : IMetaDataReader
    {
        private const string MetaFileName = "meta.xml";

        /// <summary>
        /// Deserializes the meta.xml file into an Archive object
        /// </summary>
        /// <param name="path">Path to the meta.xml file (excluding filename)</param>
        /// <returns>Archive object</returns>
        public Dwc.Text.Archive ReadMetaData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Dwc.Text.Archive));
            string fileName = Path.Combine(path, MetaFileName);
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return serializer.Deserialize(stream) as Dwc.Text.Archive;
            }
        }
    }
}
