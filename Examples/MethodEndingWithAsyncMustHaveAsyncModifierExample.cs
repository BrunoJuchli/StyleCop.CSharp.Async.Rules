using System.Threading.Tasks;

namespace Examples
{
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
