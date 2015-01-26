namespace StyleCop.CSharp
{
    using MethodVisitors;
    using System.Collections.Generic;
    using System.Linq;

    [SourceAnalyzer(typeof(CsParser))]
    public class AsyncRules : SourceAnalyzer
    {
        private readonly IEnumerable<MethodVisitorInfo> methodVisitors;

        public AsyncRules()
        {
            this.methodVisitors = new IMethodVisitor[]
            {
                new MethodEndingWithAsyncMustHaveAsyncModifier(),
                new MethodsWithAsyncModifierMustEndWithAsync(),
                new MethodsWithAsyncModifierShouldReturnAwaitable(),
            }
            .Select(mv => new MethodVisitorInfo(mv))
            .ToList();
        }

        public override void AnalyzeDocument(CodeDocument document)
        {
            var doc = (CsDocument)document;

            // skipping invalid and generated documents
            if (doc.RootElement == null || doc.RootElement.Generated)
            {
                return;
            }

            doc.WalkDocument(this.WalkMethods);
        }

        private bool WalkMethods(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType != ElementType.Method)
            {
                return true;
            }

            var method = (Method)element;

            foreach (MethodVisitorInfo methodVisitor in this.methodVisitors)
            {
                foreach (MethodViolationData violationData in methodVisitor.MethodVisitor.Visit(method))
                {
                    this.AddViolation(method, method.Location, methodVisitor.RuleName, violationData.Parameters);
                }
            }

            return true;
        }

        private class MethodVisitorInfo
        {
            public MethodVisitorInfo(IMethodVisitor methodVisitor)
            {
                this.MethodVisitor = methodVisitor;
                this.RuleName = methodVisitor.GetType().Name;
            }

            public IMethodVisitor MethodVisitor { get; private set; }

            public string RuleName { get; private set; }
        }
    }
}