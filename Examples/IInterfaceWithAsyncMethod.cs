namespace Examples
{
    using System.Threading.Tasks;

    public interface IInterfaceWithAsyncMethod
    {
        void InvalidAsync();

        TaskIsNotOne InvalidNotReturningProperTaskAsync();

        Task DoAsync();

        Task<int> DoGenericAsync();
    }
}