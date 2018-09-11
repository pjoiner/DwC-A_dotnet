using Microsoft.Extensions.Logging;
using System.Linq;

namespace DWC_A.Meta
{
    internal class CoreFileMetaData : AbstractFileMetaData, IFileMetaData
    {
        private readonly CoreFileType coreFileType;

        public CoreFileMetaData(ILogger logger, CoreFileType coreFileType):
            base(logger, coreFileType)
        {
            this.coreFileType = coreFileType ?? new CoreFileType();
            if (this.coreFileType.Id != null)
            {
                Fields = new FieldMetaData(this.coreFileType.Id, this.coreFileType.Field);
                logger.LogDebug($"Field meta data for core file {FileName}");
                Fields.ToList().ForEach(f => logger.LogDebug($"{f.Index}: {f.Term}"));
            }
        }

        public IdFieldType Id { get { return coreFileType.Id; } }

        public IFieldMetaData Fields { get; }
    }
}
