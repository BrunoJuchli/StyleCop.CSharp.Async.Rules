namespace StyleCop.CSharp
{
    using System.Collections.Generic;

    internal interface IMethodVisitor
    {
        IEnumerable<MethodViolationData> Visit(Method method);
    }
}