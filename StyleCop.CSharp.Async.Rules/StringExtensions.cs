namespace StyleCop.CSharp.Async.Rules
{
    internal static class StringExtensions
    {
        public static string RemoveGenericParantheses(this string name)
        {
            int indexOfOpeningBracket = name.IndexOf('<');
            // > 0 ==> name can't begin with a bracket...
            if (indexOfOpeningBracket > 0)
            {
                return name.Substring(0, indexOfOpeningBracket);
            }

            return name;
        }
    }
}