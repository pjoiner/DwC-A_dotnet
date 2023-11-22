using DwC_A.Meta;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwC_A.Extensions
{
    internal static class StreamReaderExtensions
    {
        /// <summary>
        /// Reads a row of data from a file using the fileMetaData to determine line endings and quotes
        /// </summary>
        /// <param name="reader">Stream reader</param>
        /// <param name="fileMetaData">File MetaData containing line ending and quotes data</param>
        /// <returns>false if this is the end of the file else true</returns>
        public static string ReadRow(this System.IO.StreamReader reader, IFileMetaData fileMetaData)
        {
            if (reader.Peek() == -1)
            {
                return null;
            }

            var line = new StringBuilder();
            char Quotes = fileMetaData.FieldsEnclosedBy.FirstOrDefault();
            if (Quotes == '\0')
            {
                line.Append(reader.ReadLine());
                return line.Flush();
            }
            char terminatorStart = fileMetaData.LinesTerminatedBy.FirstOrDefault();
            bool inQuotes = false;
            char[] c = new char[1];
            while (reader.Peek() != -1)
            {
                reader.Read(c, 0, 1);
                if (!inQuotes && c[0] == terminatorStart)
                {
                    for (int i = 1; i < fileMetaData.LineTerminatorLength; i++)
                    {
                        reader.Read();
                    }
                    return line.Flush();
                }
                if (c[0] == Quotes)
                {
                    inQuotes = !inQuotes;
                }
                line.Append(c[0]);
            }
            return line.Flush();
        }

        public async static Task<string> ReadRowAsync(this System.IO.StreamReader reader, IFileMetaData fileMetaData)
        {
            if (reader.Peek() == -1)
            {
                return null;
            }

            var line = new StringBuilder();
            char Quotes = fileMetaData.FieldsEnclosedBy.FirstOrDefault();
            if (Quotes == '\0')
            {
                line.Append(await reader.ReadLineAsync().ConfigureAwait(false));
                return line.Flush();
            }
            char terminatorStart = fileMetaData.LinesTerminatedBy.FirstOrDefault();
            bool inQuotes = false;
            char[] c = new char[1];
            while (reader.Peek() != -1)
            {
                await reader.ReadAsync(c, 0, 1).ConfigureAwait(false);
                if (!inQuotes && c[0] == terminatorStart)
                {
                    for (int i = 1; i < fileMetaData.LineTerminatorLength; i++)
                    {
                        await reader.ReadAsync(c, 0, 1).ConfigureAwait(false);
                    }
                    return line.Flush();
                }
                if (c[0] == Quotes)
                {
                    inQuotes = !inQuotes;
                }
                line.Append(c[0]);
            }
            return line.Flush();
        }

    }
}
