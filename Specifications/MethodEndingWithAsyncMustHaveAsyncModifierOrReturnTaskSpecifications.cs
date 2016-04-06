using Examples;
using Machine.Specifications;
using StyleCop.CSharp.MethodVisitors;
using System;

namespace Specifications
{
    [Subject("Methods with Async Suffix")]
    class MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTaskSpecifications : ExamplesBasedSpecifications
    {
        private static readonly Type RuleType = typeof(MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTask);

        private static readonly string ExamplesFileName = typeof(MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTaskExample).Name;

        It should_warn_when_method_does_not_have_async_suffic_and_doesnt_return_task = () =>
            ShouldHaveWarningFor(RuleType, ExamplesFileName, 7);

        It should_warn_when_method_does_not_have_async_suffic_and_returns_improper_task = () =>
            ShouldHaveWarningFor(RuleType, ExamplesFileName, 11);

        It should_not_warn_when_method_has_async_modifier = () =>
            ShouldNotHaveWarningFor(RuleType, ExamplesFileName, 16);

        It should_not_warn_when_method__has_async_modifier_and_returns_task = () =>
            ShouldNotHaveWarningFor(RuleType, ExamplesFileName, 21);

        It should_not_warn_when_method_returns_task = () =>
            ShouldNotHaveWarningFor(RuleType, ExamplesFileName, 26);

        It should_not_warn_when_method_returns_generic_task = () =>
            ShouldNotHaveWarningFor(RuleType, ExamplesFileName, 31);

        //
        // Interface Tests
        //
        private static readonly string InterfaceFileName = typeof(IInterfaceWithAsyncMethod).Name;

        It should_warn_when_interface_method_returns_void = () =>
            ShouldHaveWarningFor(RuleType, InterfaceFileName, 7);

        It should_warn_when_interface_method_returns_improper_task = () =>
            ShouldHaveWarningFor(RuleType, InterfaceFileName, 9);

        It should_not_warn_when_interface_method_returns_task = () =>
            ShouldNotHaveWarningFor(RuleType, InterfaceFileName, 11);

        It should_not_warn_when_interface_method_returns_generic_task = () =>
            ShouldNotHaveWarningFor(RuleType, InterfaceFileName, 13);
    }
}