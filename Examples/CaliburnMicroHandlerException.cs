using Caliburn.Micro;
using System.Threading.Tasks;

namespace Examples
{
    public class CaliburnMicroHandlerException : IHandleWithTask<string>
    {
        public async Task Handle(string message)
        {
            await Task.Delay(0);
        }
    }
}