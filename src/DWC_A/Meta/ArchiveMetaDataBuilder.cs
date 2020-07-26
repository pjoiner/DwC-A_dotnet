using System.IO;
using System.Xml.Serialization;

namespace DwC_A.Meta
{
    public class ArchiveMetaDataBuilder
    {
        private readonly Archive archive = new Archive();
        private readonly string fileName = "meta.xml";
        private readonly string path;

        public ArchiveMetaDataBuilder(string path)
        {
            this.path = path;
        }

        public ArchiveMetaDataBuilder CoreFile(CoreFileMetaDataBuilder coreFile)
        {
            archive.Core = coreFile.Build();
            return this;
        }

        public ArchiveMetaDataBuilder AddExtension(ExtensionFileMetaDataBuilder extension)
        {
            archive.Extension.Add(extension.Build());
            return this;
        }

        public ArchiveMetaDataBuilder MetaData(string fileName)
        {
            archive.Metadata = fileName;
            return this;
        }

        public Archive Build()
        {
            return archive;
        }

        public void Serialize()
        {
            var metaDataFileName = Path.Combine(path, fileName);
            var overrides = GetXmlAttributeOverrides();
            XmlSerializer serializer = new XmlSerializer(typeof(Archive), overrides);
            using (Stream stream = new FileStream(metaDataFileName, FileMode.Create))
            {
                serializer.Serialize(stream, Build());
            }
        }

        private XmlAttributeOverrides GetXmlAttributeOverrides()
        {
            var attributeNames = new[]
            {
                "linesTerminatedBy",
                "fieldsTerminatedBy",
                "fieldsEnclosedBy",
                "ignoreHeaderLines",
                "rowType",
                "encoding",
                "dateFormat"
            };
            var overrides = new XmlAttributeOverrides();
            foreach(var attributeName in attributeNames)
            {
                var attribute = new XmlAttributes()
                {
                    XmlDefaultValue = null,
                    XmlAttribute = new XmlAttributeAttribute()
                    { 
                        AttributeName = attributeName                        
                    }
                };
                var memberName = Capitalize(attributeName);
                overrides.Add(typeof(FileType), memberName, attribute);
            }
            return overrides;
        }

        private string Capitalize(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }
    }
}
