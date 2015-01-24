using System.Diagnostics;

namespace Specifications
{
    public interface IProcessOutputHandler
    {
        void HandleOutput(DataReceivedEventArgs e);

        void HandleError(DataReceivedEventArgs e);
    }
}