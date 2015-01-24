namespace Specifications
{
    public static class StringExtensions
    {
        public static string SubstringBetween(this string s, int startIndex, int endIndex)
        {
            return s.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
    }
}