namespace Specifications
{
    using Examples;
    using Machine.Specifications;
    using StyleCop.CSharp.MethodVisitors;
    using System;

    [Subject("Methods with async modifier")]
    class MethodsWithAsyncModifierShouldReturnAwaitableSpecifications : ExamplesBasedSpecifications
    {
        private static readonly Type RuleType = typeof(MethodsWithAsyncModifierShouldReturnAwaitable);

        private static readonly string FileName = typeof(MethodsWithAsyncModifierShouldReturnAwaitableExample).Name;

        It should_warn_when_method_returns_void = () =>
            ShouldHaveWarningFor(RuleType, FileName, 7);

        It should_not_warn_when_method_returns_awaitable = () =>
            ShouldNotHaveWarningFor(RuleType, FileName, 12);
    }
}