namespace Examples
{
    using System.Threading.Tasks;

    public class MethodEndingWithAsyncMustHaveAsyncModifierExample
    {
        public void InvalidAsync()
        {
        }

        public async Task ValidAsync()
        {
            await Task.Delay(0);
        }
    }
}
