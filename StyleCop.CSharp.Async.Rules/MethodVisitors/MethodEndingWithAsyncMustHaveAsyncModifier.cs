using System.Collections.Generic;

namespace StyleCop.CSharp.MethodVisitors
{
    internal class MethodEndingWithAsyncMustHaveAsyncModifier : IMethodVisitor
    {
        public IEnumerable<MethodViolationData> Visit(Method method)
        {
            if (method.HasAsyncSuffix())
            {
                if (!method.IsAsyncMethod())
                {
                    yield return new MethodViolationData(method);
                }
            }
        }
    }
}