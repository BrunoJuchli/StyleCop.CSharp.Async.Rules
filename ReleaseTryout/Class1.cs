using System.Threading.Tasks;

namespace ReleaseTryout
{
    // should cause some violations
    public class Class1
    {
        public static async void Foo()
        {
            await Task.Delay(0);
        }

        public static void FooAsync()
        {
        }
    }
}
