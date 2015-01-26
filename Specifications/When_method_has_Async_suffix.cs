using StyleCop.CSharp.MethodVisitors;
using System;

namespace Specifications
{
    using Examples;
    using Machine.Specifications;

    public class When_method_has_Async_suffix : ExamplesBasedSpecifications
    {
        private static readonly Type RuleType = typeof(MethodEndingWithAsyncMustHaveAsyncModifier);

        // todo generate file name (well without full path?!) from type (Name, Namespace,.. Project name?)
        private static readonly string FileName = typeof(MethodEndingWithAsyncMustHaveAsyncModifierExample).Name;
        
        It should_warn_when_method_is_missing_async_modifier = () => 
            ShouldHaveWarningFor(RuleType, FileName, 7);

        It should_not_warn_when_method_has_async_modifier = () =>
            ShouldNotHaveWarningFor(RuleType, FileName, 11);
    }
}