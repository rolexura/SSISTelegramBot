using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace XBase.TelegramBot {
    public partial class TelegramBotTaskUIForm : Form {
        public TelegramBotTaskUIForm(List<ConnectionManagerUUIDKeyValuePair> connectionsList) {
            InitializeComponent();
            CbxConnectionManagers.DataSource = connectionsList;
        }

        private void PbxAbout_Click(object sender, EventArgs e) {
            var assembly = Assembly.GetExecutingAssembly();
            var attributes = assembly.GetCustomAttributes(false);
            var copyright = string.Empty;
            var productName = string.Empty;
            foreach (var attribute in attributes) {
                switch (attribute) {
                    case AssemblyCopyrightAttribute copyrightAttribute:
                        copyright = copyrightAttribute.Copyright;
                        break;
                    case AssemblyProductAttribute productAttribute:
                        productName = productAttribute.Product;
                        break;
                }
            }
            MessageBox.Show(
                $"{productName} Version {assembly.GetName().Version}\nCopyright \u00a9 {copyright}", "About...");
        }
    }
}
