namespace Examples
{
    using System.Threading.Tasks;

    public static class MethodEndingWithAsyncMustHaveAsyncModifierExample
    {
        public static void InvalidAsync()
        {
        }

        public static async Task ValidAsync()
        {
            await Task.Delay(0);
        }
    }
}
