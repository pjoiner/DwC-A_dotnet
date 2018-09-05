using Dwc.Text;
using System.Collections.Generic;

namespace DWC_A
{
    public class CoreFileMetaData : AbstractFileMetaData, IFileMetaData
    {
        private readonly CoreFileType coreFileType;

        public CoreFileMetaData(CoreFileType coreFileType):
            base(coreFileType)
        {
            this.coreFileType = coreFileType;
            //TODO: Use a null object ???
            if(coreFileType != null)
            {
                Fields = new FieldMetaData(coreFileType.Id, coreFileType.Field);
            }
        }

        public ICollection<FieldType> FieldTypes { get { return coreFileType.Field; } }

        public IdFieldType Id { get { return coreFileType.Id; } }

        public IFieldMetaData Fields { get; }
    }
}
