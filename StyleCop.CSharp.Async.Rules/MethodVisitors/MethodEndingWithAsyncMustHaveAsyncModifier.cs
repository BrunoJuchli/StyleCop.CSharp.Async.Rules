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
                    yield return new MethodViolationData();
                }
            }
        }
    }
}