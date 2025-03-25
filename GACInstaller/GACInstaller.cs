/*
 * Copyright 2025 Rostislav Uralskyi
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.IO;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GACInstaller {
    public class GACInstaller {
        [DllImport("fusion.dll", CharSet = CharSet.Auto)]
        private static extern int CreateAssemblyCache(out IAssemblyCache ppAsmCache, int reserved);


        [STAThread]
        public static void Main(string[] args) {
            const string appName = "GAC Install Helper";

            if (args.Length < 1) {
                MessageBox.Show(
                    "Usage:\n\nTo Install: GacInstaller.exe assembly.dll\nTo Uninstall: GacInstaller.exe /u AssemblyName,Version=x.x.x.x,PublicKeyToken=abcdef1234567890",
                        appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var isUninstall = args[0].Equals("/u", StringComparison.OrdinalIgnoreCase);
            var target = isUninstall ? (args.Length > 1 ? args[1] : null) : args[0];

            if (string.IsNullOrEmpty(target)) {
                MessageBox.Show("Error: Missing assembly information.", appName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try {
                if (isUninstall) {
                    var result = UninstallFromGAC(target);
                    if (result != 0) {
                        MessageBox.Show($"Failed to remove assembly '{target}' from GAC.", appName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //if (result == 0) {
                    //    MessageBox.Show($"Assembly '{target}' removed from GAC successfully!", appName,
                    //            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //} else {
                    //    MessageBox.Show($"Failed to remove assembly '{target}' from GAC.", appName,
                    //            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}

                } else {
                    if (!File.Exists(target)) {
                        MessageBox.Show($"Error: File not found: {target}", appName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var publish = new Publish();
                    publish.GacInstall(target);
                    //MessageBox.Show("Assembly installed to GAC successfully!", appName,
                    //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}", appName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int UninstallFromGAC(string assemblyName) {
            CreateAssemblyCache(out var assemblyCache, 0);
            return assemblyCache?.UninstallAssembly(0, assemblyName, IntPtr.Zero, out _) ?? -1;
        }
    }


    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
    internal interface IAssemblyCache {
        int UninstallAssembly(int flags, [MarshalAs(UnmanagedType.LPWStr)] string assemblyName, IntPtr reserved, out int disposition);
    }
}