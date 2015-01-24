using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;

namespace Specifications
{
    public class MsBuildRunner
    {
        private static readonly TimeSpan BuildTimeout = TimeSpan.FromSeconds(30);

        private static readonly string MsBuildPath = DetermineMsBuildPath();

        public static void BuildProject(string projectFilePath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = MsBuildPath,
                Arguments = projectFilePath + " /t:Build",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process msBuildProcess = Process.Start(startInfo);
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
            double d;
            if (double.TryParse(s, out d))
            {
                return d;
            }

            return null;
        }
    }
}
