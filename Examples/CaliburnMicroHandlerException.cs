namespace Examples
{
    using Caliburn.Micro;
    using System.Threading.Tasks;

    public class CaliburnMicroHandlerException : IHandleWithTask<string>
    {
        public async Task Handle(string message)
        {
            await Task.Delay(0);
        }
    }
}