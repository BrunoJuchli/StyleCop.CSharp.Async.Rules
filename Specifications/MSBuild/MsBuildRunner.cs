using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Xml;

namespace Specifications.MSBuild
{
    internal class MsBuildRunner
    {
        private static readonly TimeSpan BuildTimeout = TimeSpan.FromSeconds(30);

        private static readonly string MsBuildPath = DetermineMsBuildPath();

        public static void BuildProject(string projectFilePath, IProcessOutputHandler outputHandler)
        {


            var startInfo = new ProcessStartInfo
            {
                FileName = MsBuildPath,
                // nr:false makes sure msbuild process ends and files are unlocked once build is done.
                Arguments = projectFilePath + " /t:Clean;Build /nr:false /property:WithAsyncRules=TRUE /p:RunCodeAnalysis=false",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };

            Process msBuildProcess = new Process
            {
                StartInfo = startInfo,
            };

            msBuildProcess.OutputDataReceived += (sender, args) => outputHandler.HandleOutput(args);
            msBuildProcess.ErrorDataReceived += (sender, args) => outputHandler.HandleError(args);
            msBuildProcess.Start();
            msBuildProcess.BeginOutputReadLine();
            msBuildProcess.BeginErrorReadLine();
            if (!msBuildProcess.WaitForExit((int)BuildTimeout.TotalMilliseconds))
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    "The msbuild process failed to terminate in {0} seconds",
                    BuildTimeout.TotalSeconds);
                throw new TimeoutException(message);
            }
        }

        private static string DetermineMsBuildPath()
        {
            const string MSBuildToolsVersionsKey = @"SOFTWARE\Microsoft\MSBuild\ToolsVersions";
            var localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            RegistryKey toolVersions = localMachineKey.OpenSubKey(MSBuildToolsVersionsKey, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.EnumerateSubKeys | RegistryRights.ReadPermissions | RegistryRights.QueryValues);
            if (toolVersions == null)
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    "msbuild seems not to be installed on this machine. Registry Key HKLM{0} was not found.",
                    MSBuildToolsVersionsKey);
                throw new InvalidOperationException(message);
            }

            string[] versionKeyNames = toolVersions.GetSubKeyNames();

            string latestVersionKeyName = versionKeyNames
                .Select(subKeyName => new KeyNameAndVersionNumber(subKeyName, TryParse(subKeyName)))
                .Where(x => x.VersionNumber.HasValue)
                .OrderByDescending(x => x.VersionNumber)
                .Select(x => x.KeyName)
                .First();

            return toolVersions.OpenSubKey(latestVersionKeyName).GetValue("MSBuildToolsPath") + "msbuild.exe";
        }

        private class KeyNameAndVersionNumber
        {
            public KeyNameAndVersionNumber(string keyName, double? versionNumber)
            {
                KeyName = keyName;
                VersionNumber = versionNumber;
            }

            public string KeyName { get; private set; }

            public double? VersionNumber { get; private set; }
        }

        private static double? TryParse(string s)
        {
            try
            {
                // XmlConvert.ToDouble always uses a dot (`.`) as decimal separator.
                return XmlConvert.ToDouble(s);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
