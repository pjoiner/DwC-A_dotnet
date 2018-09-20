namespace DwC_A.Meta
{
    internal class ExtensionFileMetaData : AbstractFileMetaData, IFileMetaData
    {
        private readonly ExtensionFileType extensionFileType;

        public ExtensionFileMetaData(ExtensionFileType extensionFileType)
            :base(extensionFileType)
        {
            this.extensionFileType = extensionFileType ?? new ExtensionFileType();
            if (this.extensionFileType.Coreid != null)
            {
                Fields = new FieldMetaData(extensionFileType.Coreid, extensionFileType.Field);
            }
        }

        public IdFieldType Id { get { return extensionFileType.Coreid; } }

        public IFieldMetaData Fields { get; }
    }
}
