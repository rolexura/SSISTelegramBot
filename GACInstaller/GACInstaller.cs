using System;
using System.IO;
using System.EnterpriseServices.Internal;
using System.Windows.Forms;

namespace GACInstaller {
    public class GACInstaller {
        [STAThread]
        public static void Main(string[] args) {
            const string appName = "GAC Install Helper";

            if (args.Length < 1) {
                MessageBox.Show(
                    "Usage:\n\nTo Install: GacInstaller.exe assembly.dll\nTo Uninstall: GacInstaller.exe /u assembly.dll",
                    appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var isUninstall = args[0].Equals("/u", StringComparison.OrdinalIgnoreCase);
            var assemblyPath = isUninstall ? (args.Length > 1 ? args[1] : null) : args[0];

            if (string.IsNullOrEmpty(assemblyPath) || !File.Exists(assemblyPath)) {
                MessageBox.Show($"Error: Assembly file not found.\n\nPath: {assemblyPath}", appName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try {
                var publish = new Publish();

                if (isUninstall) {
                    publish.GacRemove(assemblyPath);
                    MessageBox.Show("Assembly removed from GAC successfully!", appName, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                } else {
                    publish.GacInstall(assemblyPath);
                    MessageBox.Show("Assembly installed to GAC successfully!", appName, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show($"Operation failed: {ex.Message}", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}