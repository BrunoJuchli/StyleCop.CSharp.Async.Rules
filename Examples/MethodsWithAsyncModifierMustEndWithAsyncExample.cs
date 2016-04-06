namespace Examples
{
    using System.Threading.Tasks;

    public class MethodsWithAsyncModifierMustEndWithAsyncExample
    {
        public static async Task Invalid()
        {
            await Task.Delay(0);
        }

        public static async Task ValidAsync()
        {
            await Task.Delay(0);
        }
    }
}