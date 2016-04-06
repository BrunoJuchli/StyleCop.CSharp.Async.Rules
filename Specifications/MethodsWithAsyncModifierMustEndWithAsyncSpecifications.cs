namespace Specifications
{
    using Examples;
    using Machine.Specifications;
    using StyleCop.CSharp.MethodVisitors;
    using System;

    [Subject("Methods with Async Modifier")]
    class MethodsWithAsyncModifierMustEndWithAsyncSpecifications : ExamplesBasedSpecifications
    {
        private static readonly Type RuleType = typeof(MethodsWithAsyncModifierMustEndWithAsync);

        private static readonly string FileName = typeof(MethodsWithAsyncModifierMustEndWithAsyncExample).Name;

        It should_warn_when_does_not_have_async_suffix = () =>
            ShouldHaveWarningFor(RuleType, FileName, 7);

        It should_not_warn_when_method_has_async_suffix = () =>
            ShouldNotHaveWarningFor(RuleType, FileName, 12);
    }
}