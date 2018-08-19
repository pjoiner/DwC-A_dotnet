using Dwc.Text;
using DWC_A.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DWC_A
{
    public class Tokenizer : ITokenizer
    {
        private readonly IFileAttributes fileAttributes;
        private readonly bool HasQuotes;
        private readonly char Delimiter;
        private readonly char Quotes;

        public Tokenizer(IFileAttributes fileAttributes)
        {
            //TODO: Initialize fileAttributes if null
            //      Also need to check FieldsEnclosedBy and FieldsTerminatedBy
            this.fileAttributes = fileAttributes;
            this.HasQuotes = fileAttributes.FieldsEnclosedBy?.Length > 0;
            this.Delimiter = fileAttributes.FieldsTerminatedBy.FirstOrDefault();
            this.Quotes = fileAttributes.FieldsEnclosedBy.FirstOrDefault();
        }

        public IEnumerable<string> Split(string line)
        {
            StringBuilder token = new StringBuilder();
            bool inQuotes = false;

            foreach (var c in line)
            {
                if (HasQuotes && c == Quotes)
                {
                    inQuotes = inQuotes ^ true;
                }
                else if(!inQuotes && c == Delimiter)
                {
                    yield return token.Flush();
                }
                else
                {
                    token.Append(c);
                }
            }
            yield return token.Flush();
        }
    }
}
