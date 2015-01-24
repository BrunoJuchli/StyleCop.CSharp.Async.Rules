using System;
using Machine.Specifications;
using System.IO;
using System.Linq;

namespace Specifications
{
    [Subject("Example based Specifications")]
    public class ExamplesBasedSpecifications
    {
        private static string examplesProjectFilePath;

        Establish context = () =>
        {
            if (string.IsNullOrEmpty(examplesProjectFilePath))
            {
                examplesProjectFilePath = DetermineExamplesProjectFilePath();
            }
        };

        Because of = () =>
        {
            var warningsGatherer = new StyleCopWarningGatheringProcessOutputHandler();
            MsBuildRunner.BuildProject(examplesProjectFilePath, warningsGatherer);
            Console.WriteLine("done");
        };

        It should_not_fail = () =>
        {
            
        };

        private static string DetermineExamplesProjectFilePath()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var solutionDirectory = currentDirectory.Parent.Parent.Parent; // usually Project/bin/debug/foo.dll
            var examplesDirectory = solutionDirectory.GetDirectories("Examples").Single();
            var projectFiles = examplesDirectory.GetFiles("*.csproj");
            return projectFiles.Single().FullName;
        }
    }
}