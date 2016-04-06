using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Specifications.MSBuild
{
    public class StyleCopWarningGatheringProcessOutputHandler : IProcessOutputHandler
    {
        private readonly List<StyleCopBuildWarning> _buildWarnings = new List<StyleCopBuildWarning>();

        public void HandleOutput(DataReceivedEventArgs e)
        {
            // show msbuild output in test runner console
            Console.WriteLine(e.Data);

            try
            {
                this.ProcessData(e);
            }
            catch (Exception ex) // this is ugly, but throwing an exception here will block the build process for ever
            {
                Console.Error.WriteLine("unhandled error in msbuild process output handler");
                Console.Error.WriteLine(ex);
            }
        }

        public void HandleError(DataReceivedEventArgs e)
        {
        }

        private void ProcessData(DataReceivedEventArgs e)
        {
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
            if (e.Data.Contains(": warning :"))
            {
                this._buildWarnings.Add(StyleCopBuildWarningParser.Create(e.Data));
            }
        }

        public IReadOnlyCollection<StyleCopBuildWarning> ParsedWarnings
        {
            get { return this._buildWarnings; }
        }
    }
}