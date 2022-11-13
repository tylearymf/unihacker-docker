using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UniHacker
{
    public partial class Program
    {
        // Unity Arg
        public const string UNITY_PATH = nameof(UNITY_PATH);

        // Unity Hub Arg
        public const string HUB_PATH = nameof(HUB_PATH);
        public const string NEED_LOGIN = nameof(NEED_LOGIN);
        public const string DISABLE_UPDATE = nameof(DISABLE_UPDATE);

        // Common Arg
        public const string EXEC_METHOD = nameof(EXEC_METHOD);

        // Execute Method Arg
        public const string PATCH = nameof(PATCH);
        public const string RESTORE = nameof(RESTORE);
        public const string CHECK = nameof(CHECK);

        public static HashSet<string> MethodNames = new HashSet<string>() { PATCH, RESTORE, CHECK };

        public static async Task Main(string[] args)
        {
            var filePath = string.Empty;
            TryGetEnvironmentVariable(UNITY_PATH, out var unityPath);
            TryGetEnvironmentVariable(HUB_PATH, out var hubPath);
            TryGetEnvironmentVariable(EXEC_METHOD, out var execMethod);

            if (unityPath != null)
            {
                await MessageBox.Show($"Unity Argument.\n" +
                    $"\t\t{UNITY_PATH}: {unityPath}\n" +
                    $"\t\t{EXEC_METHOD}: {execMethod}\n");

                filePath = unityPath;
            }
            else if (hubPath != null)
            {
                var hasNeedLogin = TryGetEnvironmentVariable(NEED_LOGIN, out var needLogin, bool.FalseString);
                var hasDisableUpdate = TryGetEnvironmentVariable(DISABLE_UPDATE, out var disableUpdate, bool.FalseString);

                await MessageBox.Show($"UnityHub Argument.\n" +
                    $"\t\t{HUB_PATH}: {hubPath}\n" +
                    $"\t\t{EXEC_METHOD}: {execMethod}\n" +
                    $"\t\t{NEED_LOGIN}: {needLogin}{(hasNeedLogin ? "" : " (Default)")}\n" +
                    $"\t\t{DISABLE_UPDATE}: {disableUpdate}{(hasDisableUpdate ? "" : " (Default)")}\n");

                filePath = hubPath;
            }
            else
            {
                await MessageBox.Show($"Please provide {nameof(UNITY_PATH)} parameter or {nameof(HUB_PATH)} parameter");
                return;
            }

            if (!PlatformUtils.IsAdministrator)
            {
                await MessageBox.Show("Please run as an administrator.");
                return;
            }

            var patcher = PatchManager.GetPatcher(filePath, PlatformUtils.GetPlatformType());
            var status = patcher?.PatchStatus ?? PatchStatus.Unknown;
            var architectureName = MachineArchitecture.GetArchitectureName(patcher?.ArchitectureType ?? ArchitectureType.UnKnown);
            await MessageBox.Show($"Information.\n" +
                $"\t\tVersion: {patcher?.FileVersion}({architectureName})\n" +
                $"\t\tStatus: {status}\n" +
                $"\t\tPlatform: {PlatformUtils.GetPlatformType()}\n");

            if (patcher == null || patcher.PatchStatus == PatchStatus.Unknown)
            {
                await MessageBox.Show($"Unknown binary file. '{filePath}'");
                return;
            }

            if (!MethodNames.Contains(execMethod!))
            {
                await MessageBox.Show($"Unknown '{EXEC_METHOD}' parameter: {execMethod}");
                return;
            }

            try
            {
                switch (execMethod)
                {
                    case PATCH:
                        {
                            (bool success, string errorMsg) = await patcher.ApplyPatch(progress => { });
                            if (!success)
                                throw new Exception(errorMsg);
                        }
                        break;
                    case RESTORE:
                        {
                            (bool success, string errorMsg) = await patcher.RemovePatch(progress => { });
                            if (!success)
                                throw new Exception(errorMsg);
                        }
                        break;
                }
                await MessageBox.Show($"Successfully {execMethod}.");
            }
            catch (Exception ex)
            {
                await MessageBox.Show(ex.ToString());
            }
        }

        public static bool TryGetEnvironmentVariable(string variable, out string? value, string? defaultValue = null)
        {
            var target = EnvironmentVariableTarget.Process;
            value = Environment.GetEnvironmentVariable(variable, target);
            if (value == null)
            {
                if (defaultValue != null)
                {
                    value = defaultValue;
                    Environment.SetEnvironmentVariable(variable, defaultValue, target);
                }
                return false;
            }

            return true;
        }
    }
}