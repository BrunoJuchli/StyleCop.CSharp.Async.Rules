using System.Diagnostics;

namespace Specifications
{
    using FluentAssertions;
    using Machine.Specifications;
    using MSBuild;
    using Rules;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class ExamplesBasedSpecifications
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

                // sanity check
                if (!StyleCopBuildWarnings.Any())
                {
                    Console.Error.WriteLine(
                        "-- parsing of warnings must have failed. There should be at least one test with a warning. --");
                    if (Debugger.IsAttached)
                    {
                        Console.Error.WriteLine("msbuild / parsing of warnings does not work when debugging!");
                        Debugger.Break();
                    }
                }
                else
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine("-- Parsed Warnings --");
                    Console.WriteLine("---------------------");
                    Console.WriteLine();
                    foreach (StyleCopBuildWarning styleCopBuildWarning in StyleCopBuildWarnings)
                    {
                        Console.WriteLine(styleCopBuildWarning);
                    }
                }
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

            PrintRules(warningsMatchingCheckId, "The following warnings match check id {0}", checkId);

            var warningsMatchingFile = warningsMatchingCheckId
                .Where(x => x.File.Contains(fileName));

            PrintRules(warningsMatchingFile, "The following warnings match file {0}", fileName);

            var warningsMatchingLine = warningsMatchingFile.Where(x => x.Line == line);

            PrintRules(warningsMatchingLine, "The following warnings match line {0}", line);

            return warningsMatchingLine;
        }

        private static void PrintRules(IEnumerable<StyleCopBuildWarning> warningsMatchingCheckId, string format, params object[] arguments)
        {
            Console.WriteLine("_-><-_ *** _-<>-_");
            Console.WriteLine(format, arguments);
            foreach (StyleCopBuildWarning styleCopBuildWarning in warningsMatchingCheckId)
            {
                Console.WriteLine(styleCopBuildWarning);
            }
            Console.WriteLine();
        }
    }
}