using System.Collections.Generic;

namespace StyleCop.CSharp
{
    internal interface IMethodVisitor
    {
        IEnumerable<MethodViolationData> Visit(Method method);
    }
}