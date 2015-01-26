namespace Specifications
{
    using Examples;
    using Machine.Specifications;
    using StyleCop.CSharp.MethodVisitors;
    using System;

    public class When_interface_has_method_with_async_suffix : ExamplesBasedSpecifications
    {
        private static readonly Type RuleType = typeof(MethodEndingWithAsyncMustHaveAsyncModifier);

        private static readonly string FileName = typeof(IInterfaceWithAsyncMethod).Name;

        It should_not_warn_when_method_does_not_have_async_modifier = () =>
            ShouldNotHaveWarningFor(RuleType, FileName, 7);
    }
}