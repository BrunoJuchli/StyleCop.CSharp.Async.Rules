namespace StyleCop.CSharp.Async.Rules
{
    internal class ViolationData
    {
        public ICodeElement CodeElement { get; private set; }

        public CodeLocation CodeLocation { get; private set; }

        public ViolationData(ICodeElement codeElement, CodeLocation codeLocation)
        {
            CodeElement = codeElement;
            CodeLocation = codeLocation;
        }
    }
}