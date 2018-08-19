using DWC_A.Extensions;
using System.Collections.Generic;
using System.Text;

namespace DWC_A
{
    public class Tokenizer : ITokenizer
    {
        private readonly bool HasQuotes;
        private readonly char Delimiter;
        private readonly char Quotes;

        public Tokenizer(bool hasQuotes = false, char delimiter = ',', char quotes = '"')
        {
            this.HasQuotes = hasQuotes;
            this.Delimiter = delimiter;
            this.Quotes = quotes;
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
