using Examples;
using Machine.Specifications;

namespace Specifications
{
    public class MethodEndingWithAsyncMustHaveAsyncModifierSpecification : ExamplesBasedSpecifications
    {
        private static readonly string CheckId = typeof(MethodEndingWithAsyncMustHaveAsyncModifierSpecification).Name;

        // todo generate file name (well without full path?!) from type (Name, Namespace,.. Project name?)
        private static readonly string FileName = typeof(MethodEndingWithAsyncMustHaveAsyncModifierExample).Name;

        Establish context = () =>
        {
            // todo get method name / line number by reflection:
            // MethodEndingWithAsyncMustHaveAsyncModifierExample.InvalidAsync();
        };

        It should_warn_when_method_is_missing_async_modifier = () => ShouldHaveWarningFor(CheckId, FileName, 7);

        It should_not_warn_when_method_has_async_modifier = () => ShouldNotHaveWarningFor(CheckId, FileName, 11);
    }
}