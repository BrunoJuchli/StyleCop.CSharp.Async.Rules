namespace StyleCop.CSharp.MethodVisitors
{
    using System.Collections.Generic;

    internal class MethodEndingWithAsyncMustHaveAsyncModifier : IMethodVisitor
    {
        public IEnumerable<MethodViolationData> Visit(Method method)
        {
            if (method.HasAsyncSuffix())
            {
                if (!method.IsAsyncMethod())
                {
                    if (!IsPartOfInterface(method))
                    {
                        yield return new MethodViolationData();
                    }
                }
            }
        }

        private bool IsPartOfInterface(Method method)
        {
            ICodePart current = method.Parent;
            while (!(current is ClassBase))
            {
                current = current.Parent;
            }

            return current is Interface;
        }
    }
}