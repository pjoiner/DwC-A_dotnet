using System.Collections.Generic;

namespace DwC_A
{
    public interface ITokenizer
    {
        IEnumerable<string> Split(string line);
    }
}