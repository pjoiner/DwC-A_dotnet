using System.Collections.Generic;

namespace DWC_A
{
    public interface ITokenizer
    {
        IEnumerable<string> Split(string line);
    }
}