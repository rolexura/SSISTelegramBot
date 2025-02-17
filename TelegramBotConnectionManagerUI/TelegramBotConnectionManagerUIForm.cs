using System;
using System.Windows.Forms;

namespace XBase.TelegramBot {
    public partial class TelegramBotConnectionManagerUIForm : Form {
        // Default constructor
        public TelegramBotConnectionManagerUIForm() {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}