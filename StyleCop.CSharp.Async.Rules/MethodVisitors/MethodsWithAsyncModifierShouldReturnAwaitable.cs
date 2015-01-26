namespace StyleCop.CSharp.MethodVisitors
{
    using System.Collections.Generic;

    internal class MethodsWithAsyncModifierShouldReturnAwaitable : IMethodVisitor
    {
        public IEnumerable<MethodViolationData> Visit(Method method)
        {
            if (method.IsAsyncMethod())
            {
                if (method.ReturnType.Text == "void")
                {
                    yield return new MethodViolationData();
                }
            }
        }
    }
}