
namespace Specifications.MSBuild
{
    public static class StyleCopBuildWarningParser
    {
        private const char Colon = ':';
        private const char Comma = ',';

        public static StyleCopBuildWarning Create(string warningText)
        {
            var warning = new StyleCopBuildWarning();

            // now parse this:
            // C:\development\StyleCop.CSharp.Async.Rules\Examples\CaliburnMicroHandlerException.cs(1,1): warning : SA1633 : CSharp.Documentation : The file has no header, the header Xml is invalid, or the header is not located at the top of the file. [C:\development\StyleCop.CSharp.Async.Rules\Examples\Examples.csproj]
            // Caution: sometimes it's also:
            // C:\development\StyleCop.CSharp.Async.Rules\Examples\CaliburnMicroHandlerException.cs(1,1,3,10): warning : (....... rest of the text)

            int firstOpeningParanthesisIndex = warningText.IndexOf('('); // marks end of "File" entry
            int commaAfterOpeningParanthesisIndex = warningText.IndexOf(Comma, firstOpeningParanthesisIndex); // marks end of "Line" entry
            int secondCommaAfterOpeningParanthesisIndex = warningText.IndexOf(Comma, commaAfterOpeningParanthesisIndex + 1); // marks end of "Column" entry
            int closingParanthesisAfterCommaIndex = warningText.IndexOf(')', commaAfterOpeningParanthesisIndex);

            int indexOfColumnEnd;
            if (secondCommaAfterOpeningParanthesisIndex < 0 ||
                secondCommaAfterOpeningParanthesisIndex > closingParanthesisAfterCommaIndex)
            {
                indexOfColumnEnd = closingParanthesisAfterCommaIndex;
            }
            else
            {
                indexOfColumnEnd = secondCommaAfterOpeningParanthesisIndex;
            }

            int firstColonAfterClosingParanthesis = warningText.IndexOf(Colon, closingParanthesisAfterCommaIndex); // marks begin of : warning :
            int secondColonAfterClosingParanthesis = warningText.IndexOf(Colon, firstColonAfterClosingParanthesis + 1); // marks end of : warning : // begin of "Description" entry
            int thirdColonAfterClosingParanthesis = warningText.IndexOf(Colon, secondColonAfterClosingParanthesis + 1); // marks end of " SA1633 " (for example)
            int fourthColonAFterClosingPranthesis = warningText.IndexOf(Colon, thirdColonAfterClosingParanthesis + 1); // marks end of " CSharp.Documentation " (for example)
            int openingBracketAfterSecondColon = warningText.IndexOf('[', fourthColonAFterClosingPranthesis); // marks begin of "Project" entry
            int closingBracketAfterOpeningBracket = warningText.IndexOf(']', openingBracketAfterSecondColon); // marks end of "Project" end

            warning.File = warningText.Substring(0, firstOpeningParanthesisIndex).Trim();

            var lineString = warningText.SubstringBetween(firstOpeningParanthesisIndex, commaAfterOpeningParanthesisIndex).Trim();
            warning.Line = int.Parse(lineString);

            string columnString = warningText.SubstringBetween(commaAfterOpeningParanthesisIndex, indexOfColumnEnd).Trim();
            warning.Column = int.Parse(columnString);

            warning.CheckId = warningText.SubstringBetween(secondColonAfterClosingParanthesis, thirdColonAfterClosingParanthesis).Trim();
            warning.CheckNameSpace = warningText.SubstringBetween(thirdColonAfterClosingParanthesis, fourthColonAFterClosingPranthesis).Trim();
            warning.Description = warningText.SubstringBetween(fourthColonAFterClosingPranthesis, openingBracketAfterSecondColon).Trim();

            warning.Project = warningText.SubstringBetween(openingBracketAfterSecondColon, closingBracketAfterOpeningBracket).Trim();

            return warning;
        }
    }
}