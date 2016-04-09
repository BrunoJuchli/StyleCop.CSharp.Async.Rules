using System.Threading.Tasks;

namespace ReleaseTryout
{
    // should cause some violations
    public class Class1
    {
        public static async void InvalidFoo()
        {
            await Task.Delay(0);
        }

        public static void InvalidFooAsync()
        {
        }

        public static Task ValidFooAsync()
        {
            return Task.FromResult(1);
        }
    }
}
