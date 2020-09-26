using System.Text.RegularExpressions;

namespace DwC_A.Terms
{
    public partial class Terms
    {
        public static string ShortName(string term)
        {
            if (term == null)
            {
                return null;
            }
            Regex regex = new Regex("[^/]+$");
            var match = regex.Match(term);
            return string.IsNullOrEmpty(match.Value) ? term : match.Value;
        }
    }
}
