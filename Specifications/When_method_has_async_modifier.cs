namespace Specifications
{
    using Examples;
    using Machine.Specifications;
    using StyleCop.CSharp.MethodVisitors;
    using System;

    public class When_method_has_async_modifier : ExamplesBasedSpecifications
    {
        private static readonly Type MustHaveAsyncModifierRuleType = typeof(MethodEndingWithAsyncMustHaveAsyncModifier);
        private static readonly string MustHaveAsyncModifierExampleFileName = typeof(MethodEndingWithAsyncMustHaveAsyncModifierExample).Name;
        
        It should_warn_when_method_is_missing_async_modifier = () => 
            ShouldHaveWarningFor(MustHaveAsyncModifierRuleType, MustHaveAsyncModifierExampleFileName, 7);

        It should_not_warn_when_method_has_async_modifier = () =>
            ShouldNotHaveWarningFor(MustHaveAsyncModifierRuleType, MustHaveAsyncModifierExampleFileName, 11);

        private static readonly Type ShouldReturnAwaitableRuleType = typeof(MethodsWithAsyncModifierShouldReturnAwaitable);

        private static readonly string ShouldReturnAwaitableExampleFileName = typeof(MethodsWithAsyncModifierShouldReturnAwaitableExample).Name;

        It should_warn_when_method_returns_void = () =>
            ShouldHaveWarningFor(ShouldReturnAwaitableRuleType, ShouldReturnAwaitableExampleFileName, 7);

        It should_not_warn_when_method_returns_awaitable = () =>
            ShouldNotHaveWarningFor(ShouldReturnAwaitableRuleType, ShouldReturnAwaitableExampleFileName, 12);
    }
}