using Dwc.Text;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DWC_A.Meta
{
    public abstract class AbstractFileMetaData
    {
        private readonly FileType fileType;
        private readonly ILogger logger;

        public AbstractFileMetaData(ILogger logger, FileType fileType)
        {
            this.logger = logger;
            if(fileType == null)
            {
                logger.LogDebug("No file attributes found creating default attributes");
                this.fileType = new FileType();
            }
            else
            {
                this.fileType = fileType;
            }
            logger.LogDebug($"Using file attributes for file {FileName}" +
                $" Encoding: {this.fileType.Encoding}" +
                $" LinesTerminatedBy: {this.fileType.LinesTerminatedBy}" +
                $" FieldsTerminatedBy: {this.fileType.FieldsTerminatedBy}" +
                $" FieldsEnclosedBy: {this.fileType.FieldsEnclosedBy}" +
                $" HeaderRows: {this.fileType.IgnoreHeaderLines}");
        }

        public string FileName { get { return fileType.Files?.FirstOrDefault(); } }

        public string RowType { get { return fileType.RowType; } }

        public Encoding Encoding { get { return Encoding.GetEncoding(fileType.Encoding); } }

        public string LinesTerminatedBy { get { return Regex.Unescape(fileType.LinesTerminatedBy); } }

        public string FieldsTerminatedBy { get { return Regex.Unescape(fileType.FieldsTerminatedBy); } }

        public string FieldsEnclosedBy { get { return Regex.Unescape(fileType.FieldsEnclosedBy); } }

        public string DateFormat { get { return fileType.DateFormat; } }

        public int LineTerminatorLength { get { return Encoding.GetByteCount(LinesTerminatedBy); } }

        public int HeaderRowCount
        {
            get
            {
                if (!Int32.TryParse(fileType.IgnoreHeaderLines, out int headerRowCount))
                {
                    return 0;
                }
                return headerRowCount;
            }
        }
    }
}
