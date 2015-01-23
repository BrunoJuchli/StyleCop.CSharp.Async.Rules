namespace StyleCop.CSharp.Async.Rules
{
    // todo: completely rewrite
    // current source code (slightly altered) taken from https://stylecop.codeplex.com/workitem/7474 / credits https://www.codeplex.com/site/users/view/LukasK2000
    [SourceAnalyzer(typeof(CsParser))]
    public class AsyncAwaitAnalyzer : SourceAnalyzer
    {
        public override void AnalyzeDocument(CodeDocument document)
        {
            var doc = (CsDocument)document;

            // skipping invalid and generated documents
            if (doc.RootElement == null || doc.RootElement.Generated)
            {
                return;
            }

            doc.WalkDocument(this.CheckAsyncMethodNamesForEndingWithAsyncText);
            doc.WalkDocument(this.CheckMethodsEndingWithAsyncForAsyncModifier);
        }

        private bool CheckAsyncMethodNamesForEndingWithAsyncText(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType != ElementType.Method)
            {
                return true;
            }
            
            Method methodElement = (Method)element;

            // exception for CaliburnMicro - we have to use its naming without Async
            if (methodElement.Name == "Handle")
            {
                return true;
            }

            if (this.IsMethodAsAsyncDeclared(methodElement) && !this.IsMethodAsAsyncNamed(methodElement))
            {
                // add violation
                this.AddViolation(methodElement, methodElement.Location, "AsyncMethodsMustEndWithAsync");
            }

            // continue walking in order to find all classes in file
            return true;
        }

        /// <summary>
        /// Checks whether specified element conforms custom rule CR0001.
        /// </summary>
        private bool CheckMethodsEndingWithAsyncForAsyncModifier(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType != ElementType.Method)
            {
                return true;
            }

            // this rule does not count for interfaces (there are async marks missing)
            if (element.Parent as CsElement != null && ((CsElement)element.Parent).ElementType == ElementType.Interface)
            {
                return true;
            }

            Method methodElement = (Method)element;
            if (this.IsMethodAsAsyncNamed(methodElement) && !this.IsMethodAsAsyncDeclared(methodElement))
            {
                // add violation
                this.AddViolation(methodElement, methodElement.Location, "MethodsEndingWithAsyncMustBeAsync");
            }

            // continue walking in order to find all classes in file
            return true;
        }

        private bool IsMethodAsAsyncDeclared(Method method)
        {
            return method.Declaration.ContainsModifier(new[] { CsTokenType.Async });
        }

        private bool IsMethodAsAsyncNamed(Method method)
        {
            string name = method.Name;

            // fix for generics (it is part of the name)
            if (name.Contains("<"))
            {
                name = name.Substring(0, name.IndexOf('<'));
            }

            return name.EndsWith("Async");
        }
    }
}