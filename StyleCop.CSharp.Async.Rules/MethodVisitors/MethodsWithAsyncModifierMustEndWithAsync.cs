namespace StyleCop.CSharp.Async.Rules.MethodVisitors
{
    using System.Collections.Generic;

    internal class MethodsWithAsyncModifierMustEndWithAsync : IMethodVisitor
    {
        public IEnumerable<MethodViolationData> Visit(Method method)
        {
            //// todo: can we somehow handle the CaliburnMicro exception without ignoring other "handle" methods, too?
            ////// exception for CaliburnMicro - we have to use its naming without Async
            ////if (method.Name == "Handle")
            ////{
            ////    return true;
            ////}

            if (method.IsAsyncMethod())
            {
                if (!method.HasAsyncSuffix())
                {
                    yield return new MethodViolationData(method);
                }
            }
        }
    }
}