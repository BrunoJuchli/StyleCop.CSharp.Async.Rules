using System.Collections.Generic;

namespace StyleCop.CSharp.MethodVisitors
{
    internal class MethodsWithAsyncModifierShouldReturnAwaitable : IMethodVisitor
    {
        public IEnumerable<MethodViolationData> Visit(Method method)
        {
            if (method.IsAsyncMethod())
            {
                if (method.ReturnType.Text == "void")
                {
                    yield return new MethodViolationData(method);
                }
            }
        }
    }
}