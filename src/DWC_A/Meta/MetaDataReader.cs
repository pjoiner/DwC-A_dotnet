using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DWC_A.Meta
{
    internal class MetaDataReader : IMetaDataReader
    {
        private const string MetaFileName = "meta.xml";
        private readonly ILogger logger;

        public MetaDataReader(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Deserializes the meta.xml file into an Archive object
        /// </summary>
        /// <param name="path">Path to the meta.xml file (excluding filename)</param>
        /// <returns>Archive object</returns>
        public Archive ReadMetaData(string path)
        {
            string fileName = Path.Combine(path, MetaFileName);
            if (File.Exists(fileName))
            {
                return DeserializeMetaDataFile(fileName);
            }
            return DefaultMetaData(path);
        }

        private Archive DefaultMetaData(string path)
        {
            logger.LogDebug("No meta.xml metadata file found.  Creating default metadata");
            var fileNames = Directory.GetFiles(path);
            var coreFileName = fileNames.Single();
            var archive = new Archive()
            {
                Core = new CoreFileType()
            };
            archive.Core.Files.Add(coreFileName);
            return archive;
        }

        private Archive DeserializeMetaDataFile(string fileName)
        {
            logger.LogDebug($"Reading meta.xml metadata for file {fileName}");
            XmlSerializer serializer = new XmlSerializer(typeof(Archive));
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return serializer.Deserialize(stream) as Archive;
            }
        }
    }
}
