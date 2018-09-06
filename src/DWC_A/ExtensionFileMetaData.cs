using System.Collections.Generic;
using Dwc.Text;

namespace DWC_A
{
    public class ExtensionFileMetaData : AbstractFileMetaData, IFileMetaData
    {
        private readonly ExtensionFileType extensionFileType;

        public ExtensionFileMetaData(ExtensionFileType extensionFileType)
            :base(extensionFileType)
        {
            this.extensionFileType = extensionFileType;
            Fields = new FieldMetaData(extensionFileType.Coreid, extensionFileType.Field);
        }

        public IdFieldType Id { get { return extensionFileType.Coreid; } }

        public IFieldMetaData Fields { get; }
    }
}
