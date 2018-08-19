using Dwc.Text;
using System.IO;
using System.Xml.Serialization;

namespace DwC_A.Meta
{
    public class MetaDataReader
    {
        private const string MetaFileName = "meta.xml";

        /// <summary>
        /// Deserializes the meta.xml file into an Archive object
        /// </summary>
        /// <param name="path">Path to the meta.xml file (excluding filename)</param>
        /// <returns>Archive object</returns>
        public Archive ReadMetaData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Archive));
            string fileName = Path.Combine(path, MetaFileName);
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return serializer.Deserialize(stream) as Archive;
            }
        }
    }
}
