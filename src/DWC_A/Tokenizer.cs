using DwC_A.Meta;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    internal class Tokenizer : ITokenizer
    {
        private readonly IFileMetaData fileMetaData;
        private readonly bool HasQuotes;
        private readonly char Delimiter;
        private readonly char Quotes;

        public Tokenizer(IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData ?? throw new ArgumentNullException(nameof(fileMetaData));
            this.HasQuotes = fileMetaData.FieldsEnclosedBy.Length > 0;
            this.Delimiter = fileMetaData.FieldsTerminatedBy.FirstOrDefault();
            this.Quotes = fileMetaData.FieldsEnclosedBy.FirstOrDefault();
        }

        public IEnumerable<string> Split(string line)
        {
            bool inQuotes = false;
            bool isData = false;
            int start = 0;
            int end = 0;
            for (int idx = 0; idx < line.Length; idx++)
            {
                var c = line[idx];
                if (HasQuotes && c == Quotes)
                {
                    if (inQuotes)
                    {
                        isData = false;
                        end = idx - 1;
                    }
                    inQuotes = !inQuotes;
                }
                else if(!inQuotes && c == Delimiter)
                {
                    isData = false;
                    yield return line.Substring(start, (end + 1) - start);
                    if(idx == line.Length - 1)
                    {
                        start = idx;
                        end = start - 1;
                    }
                    else
                    {
                        start = end = idx + 1;
                    }
                }
                else
                {
                    if (isData)
                    {
                        end = idx;
                    }
                    else
                    {
                        isData = true;
                        start = idx;
                    }
                }
            }
            yield return line.Substring(start, (end + 1) - start);
        }
    }
}
