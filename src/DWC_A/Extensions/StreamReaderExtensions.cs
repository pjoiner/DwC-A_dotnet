using DwC_A.Meta;
using System.Linq;
using System.Text;

namespace DwC_A.Extensions
{
    internal static class StreamReaderExtensions
    {
        /// <summary>
        /// Reads a row of data from a file using the fileMetaData to determine line endings and quotes
        /// </summary>
        /// <param name="reader">Stream reader</param>
        /// <param name="fileMetaData">File MetaData containing line ending and quotes data</param>
        /// <param name="line">StringBuffer to fill</param>
        /// <returns>false if this is the end of the file else true</returns>
        public static bool ReadRow(this System.IO.StreamReader reader, IFileMetaData fileMetaData, ref StringBuilder line)
        {
            if (reader.Peek() == -1)
            {
                return false;
            }

            char Quotes = fileMetaData.FieldsEnclosedBy.FirstOrDefault();
            if (Quotes == '\0')
            {
                line.Append(reader.ReadLine());
                return true;
            }
            char terminatorStart = fileMetaData.LinesTerminatedBy.FirstOrDefault();
            bool inQuotes = false;
            char c;
            while(reader.Peek() != -1)
            {
                c = (char)reader.Read();
                if (!inQuotes && c == terminatorStart)
                {
                    for (int i = 1; i < fileMetaData.LineTerminatorLength; i++)
                    {
                        reader.Read();
                    }
                    return true;
                }
                if (c == Quotes)
                {
                    inQuotes = !inQuotes;
                }
                line.Append(c);
            }
            return true;
        }
    }
}
