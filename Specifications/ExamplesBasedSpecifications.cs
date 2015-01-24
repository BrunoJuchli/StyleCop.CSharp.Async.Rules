using FluentAssertions;
using Machine.Specifications;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Specifications
{
    [Subject("Example based Specifications")]
    public class ExamplesBasedSpecifications
    {
        private static bool hasBuildBeenRun = false;

        private static string examplesProjectFilePath;

        protected static IReadOnlyCollection<StyleCopBuildWarning> StyleCopBuildWarnings;
            
        Establish context = () =>
        {
            if (!hasBuildBeenRun)
            {
                examplesProjectFilePath = DetermineExamplesProjectFilePath();
                var warningsGatherer = new StyleCopWarningGatheringProcessOutputHandler();
                MsBuildRunner.BuildProject(examplesProjectFilePath, warningsGatherer);
                StyleCopBuildWarnings = warningsGatherer.ParsedWarnings;
            }
        };

        protected static void ShouldHaveWarningFor(string checkId, string fileName, int line)
        {
            GetWarningsFor(checkId, fileName, line)
                .Should().HaveCount(1);
        }

        protected static void ShouldNotHaveWarningFor(string checkId, string fileName, int line)
        {
            GetWarningsFor(checkId, fileName, line)
                .Should().BeEmpty();
        }

        private static string DetermineExamplesProjectFilePath()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var solutionDirectory = currentDirectory.Parent.Parent.Parent; // usually Project/bin/debug/foo.dll
            var examplesDirectory = solutionDirectory.GetDirectories("Examples").Single();
            var projectFiles = examplesDirectory.GetFiles("*.csproj");
            return projectFiles.Single().FullName;
        }

        private static IEnumerable<StyleCopBuildWarning> GetWarningsFor(string checkId, string fileName, int line)
        {
            return StyleCopBuildWarnings
                .Where(x => x.CheckId == checkId)
                .Where(x => x.File.Contains(fileName))
                .Where(x => x.Line == line);
        }
    }
}