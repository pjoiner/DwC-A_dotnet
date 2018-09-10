using Dwc.Text;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DWC_A.Meta
{
    internal class ExtensionFileMetaData : AbstractFileMetaData, IFileMetaData
    {
        private readonly ExtensionFileType extensionFileType;
        private readonly ILogger logger;

        public ExtensionFileMetaData(ILogger logger, ExtensionFileType extensionFileType)
            :base(logger, extensionFileType)
        {
            this.logger = logger;
            this.extensionFileType = extensionFileType ?? new ExtensionFileType();
            if (this.extensionFileType.Coreid != null)
            {
                Fields = new FieldMetaData(extensionFileType.Coreid, extensionFileType.Field);
                logger.LogDebug($"Field meta data for extension file {FileName}");
                Fields.ToList().ForEach(f => logger.LogDebug($"{f.Index}: {f.Term}"));
            }
        }

        public IdFieldType Id { get { return extensionFileType.Coreid; } }

        public IFieldMetaData Fields { get; }
    }
}
