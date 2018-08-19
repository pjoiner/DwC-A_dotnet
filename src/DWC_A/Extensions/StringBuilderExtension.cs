using System.Text;

namespace DWC_A.Extensions
{
    internal static class StringBuilderExtension
    {
        public static string Flush(this StringBuilder stringBuilder)
        {
            string toString = stringBuilder.ToString();
            stringBuilder.Clear();
            return toString;
        }
    }
}
