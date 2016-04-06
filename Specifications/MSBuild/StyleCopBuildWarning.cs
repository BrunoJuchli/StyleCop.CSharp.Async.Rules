using System.Globalization;

namespace Specifications.MSBuild
{
    public class StyleCopBuildWarning
    {
        public string Description { get; set; }

        public string File { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public string Project { get; set; }

        public string CheckId { get; set; }

        public string CheckNameSpace { get; set; }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "'{0}:{1}' in Project '{2}' at {3}:{4}:{5} with {6}",
                CheckId,
                CheckNameSpace,
                Project,
                File,
                Line,
                Column,
                Description);
        }
    }
}