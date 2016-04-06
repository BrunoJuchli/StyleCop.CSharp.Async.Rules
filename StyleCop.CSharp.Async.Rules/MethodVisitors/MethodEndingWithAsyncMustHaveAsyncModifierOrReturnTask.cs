namespace StyleCop.CSharp.MethodVisitors
{
    using System.Collections.Generic;

    internal class MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTask : IMethodVisitor
    {
        public IEnumerable<MethodViolationData> Visit(Method method)
        {
            if (method.HasAsyncSuffix())
            {
                if (IsPartOfInterface(method))
                {
                    if (!ReturnsTask(method))
                    {
                        yield return new MethodViolationData();
                    }
                }
                else
                {
                    if (!method.IsAsyncMethod() && !ReturnsTask(method))
                    {
                        yield return new MethodViolationData();
                    }
                }
            }
        }

        private static bool ReturnsTask(Method method)
        {
            return method.ReturnType.Text.RemoveGenericParantheses() == "Task";
        }

        private static bool IsPartOfInterface(Method method)
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