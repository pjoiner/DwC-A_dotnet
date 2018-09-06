using Dwc.Text;

namespace DWC_A
{
    public class CoreFileMetaData : AbstractFileMetaData, IFileMetaData
    {
        private readonly CoreFileType coreFileType;

        public CoreFileMetaData(CoreFileType coreFileType):
            base(coreFileType)
        {
            this.coreFileType = coreFileType ?? new CoreFileType();
            if (this.coreFileType.Id != null)
            {
                Fields = new FieldMetaData(this.coreFileType.Id, this.coreFileType.Field);
            }
        }

        public IdFieldType Id { get { return coreFileType.Id; } }

        public IFieldMetaData Fields { get; }
    }
}
