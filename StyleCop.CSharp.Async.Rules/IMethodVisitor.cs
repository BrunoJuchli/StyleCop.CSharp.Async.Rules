namespace StyleCop.CSharp.Async.Rules
{
    using System.Collections.Generic;

    internal interface IMethodVisitor
    {
        IEnumerable<MethodViolationData> Visit(Method method);
    }
}