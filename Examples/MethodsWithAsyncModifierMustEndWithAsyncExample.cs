namespace Examples
{
    using System.Threading.Tasks;

    public class MethodsWithAsyncModifierMustEndWithAsyncExample
    {
        public async Task Invalid()
        {
            await Task.Delay(0);
        }

        public async Task ValidAsync()
        {
            await Task.Delay(0);
        }
    }
}