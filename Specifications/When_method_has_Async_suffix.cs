namespace Specifications
{
    using Examples;
    using Machine.Specifications;
    using StyleCop.CSharp.MethodVisitors;
    using System;

    public class When_method_has_Async_suffix : ExamplesBasedSpecifications
    {
        private static readonly Type RuleType = typeof(MethodsWithAsyncModifierMustEndWithAsync);

        private static readonly string FileName = typeof(MethodsWithAsyncModifierMustEndWithAsyncExample).Name;
        
        It should_warn_when_method_does_not_have_Async_suffix = () => 
            ShouldHaveWarningFor(RuleType, FileName, 7);

        It should_not_warn_when_method_has_Async_suffix = () =>
            ShouldNotHaveWarningFor(RuleType, FileName, 12);
    }
}