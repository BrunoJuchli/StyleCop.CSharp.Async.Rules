using System.Collections.Generic;

namespace StyleCop.CSharp.Async.Rules
{
    internal interface IMethodVisitor
    {
        IEnumerable<ViolationData> Visit(Method methodElement);
    }
}