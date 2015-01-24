using System.Threading.Tasks;

namespace Examples
{
    public class MethodsWithAsyncModifierShouldReturnAwaitableExample
    {
        public async void InvalidAsync()
        {
            await Task.Delay(0);
        }

        public async Task ValidAsync()
        {
            await Task.Delay(0);
        }
    }
}