using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UniHacker
{
    public partial class Program
    {
        public const string UNITY_PATH = nameof(UNITY_PATH);
        public const string EXEC_METHOD = nameof(EXEC_METHOD);

        public const string PATCH = nameof(PATCH);
        public const string REVERT = nameof(REVERT);
        public const string CHECK = nameof(CHECK);

        public static HashSet<string> MethodNames = new HashSet<string>() { PATCH, REVERT, CHECK };

        public static async Task Main(string[] args)
        {
            var unityPath = Environment.GetEnvironmentVariable(UNITY_PATH, EnvironmentVariableTarget.Process);
            var execMethod = Environment.GetEnvironmentVariable(EXEC_METHOD, EnvironmentVariableTarget.Process);
            MessageBox.Show($"Argument.\n" +
                $"\t\t{UNITY_PATH}:{unityPath}\n" +
                $"\t\t{EXEC_METHOD}:{execMethod}\n");

            if (!PlatformUtils.IsAdministrator)
            {
                MessageBox.Show("Please run as an administrator.");
                return;
            }

            var unityFilePath = Path.Combine(unityPath, "Editor/Unity");
            if (!File.Exists(unityFilePath))
            {
                MessageBox.Show($"The Unity file does not exist. '{unityFilePath}'");
                return;
            }

            var patcher = PatchManager.GetPatcher(unityFilePath, PlatformUtils.GetPlatformType());
            var status = patcher?.PatchStatus ?? PatchStatus.Unknown;
            MessageBox.Show($"Info.\n" +
                $"\t\tVersion:{patcher?.FileVersion}\n" +
                $"\t\t{MachineArchitecture.GetArchitectureName(patcher?.ArchitectureType ?? ArchitectureType.UnKnown)}\n" +
                $"\t\tStatus: {status}\n" +
                $"\t\tPlatform:{PlatformUtils.GetPlatformType()}\n");

            if (patcher == null || patcher.PatchStatus == PatchStatus.Unknown)
            {
                MessageBox.Show($"Unknown binary file. '{unityFilePath}'");
                return;
            }

            if (!MethodNames.Contains(execMethod))
            {
                MessageBox.Show($"Unknown '{EXEC_METHOD}' parameter: {execMethod}");
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
                    case REVERT:
                        {
                            (bool success, string errorMsg) = await patcher.RemovePatch(progress => { });
                            if (!success)
                                throw new Exception(errorMsg);
                        }
                        break;
                }
                MessageBox.Show($"Successfully {execMethod}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}