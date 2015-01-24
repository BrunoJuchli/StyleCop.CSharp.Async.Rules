using System;
using System.Diagnostics;
using System.Linq;

namespace Specifications
{
    public class StyleCopWarningGatheringProcessOutputHandler : IProcessOutputHandler
    {
        public void HandleOutput(DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);

            if (string.IsNullOrEmpty(e.Data))
            {
                return;
            }

            // all warnings are reported a second time at the end of the build. These are preceded by 2 whitespaces. We can ignore them.
            if (char.IsWhiteSpace(e.Data.First()))
            {
                return;
            }

            //: warning : catches only stylecop warnings.
            //"normal" CS warnings look like : warning CS1030:
            //"stylecop" warning look like : warning : SAXXX : ,...
            int warningIndex = e.Data.IndexOf(": warning :");
            if (warningIndex > 0)
            {
                //Console.WriteLine(e.Data);
            }
        }

        public void HandleError(DataReceivedEventArgs e)
        {
        }


    }
}