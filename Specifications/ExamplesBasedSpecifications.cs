namespace Specifications
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Machine.Specifications;
    using MSBuild;
    using Rules;

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
                hasBuildBeenRun = true;
            }
        };

        protected static void ShouldHaveWarningFor(Type rule, string fileName, int line)
        {
            GetWarningsFor(rule, fileName, line)
                .Should().HaveCount(1);
        }

        protected static void ShouldNotHaveWarningFor(Type rule, string fileName, int line)
        {
            GetWarningsFor(rule, fileName, line)
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

        private static IEnumerable<StyleCopBuildWarning> GetWarningsFor(Type rule, string fileName, int line)
        {
            string checkId = RulesRegistry.Rules[rule].CheckId;

            var warningsMatchingCheckId = StyleCopBuildWarnings
                .Where(x => x.CheckId == checkId);
                
            return warningsMatchingCheckId
                .Where(x => x.File.Contains(fileName))
                .Where(x => x.Line == line);
        }
    }
}