namespace Examples
{
    using System.Threading.Tasks;

    public static class MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTaskExample
    {
        public static void InvalidAsync()
        {
        }

        public static TaskIsNotOne InvalidDoesNotReturnProperTaskAsync()
        {
            return null;
        }

        public static async void ValidReturnsVoid()
        {
            await Task.Delay(0);
        }

        public static async Task ValidAsync()
        {
            await Task.Delay(0);
        }

        public static Task ValidReturnsTaskAsync()
        {
            return Task.Delay(0);
        }

        public static Task<int> ValidReturnsGenericTaskAsync()
        {
            return Task.FromResult(5);
        }
    }
}
