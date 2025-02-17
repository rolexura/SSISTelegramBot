
namespace XBase.TelegramBot {
    partial class TelegramBotTaskUIForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelegramBotTaskUIForm));
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.CbxConnectionManagers = new System.Windows.Forms.ComboBox();
            this.NudRequestTimeoutMS = new System.Windows.Forms.NumericUpDown();
            this.LblConnectionManager = new System.Windows.Forms.Label();
            this.LblRequestTimeoutMS = new System.Windows.Forms.Label();
            this.LblZeroMeansInfinite = new System.Windows.Forms.Label();
            this.LblMessagePattern = new System.Windows.Forms.Label();
            this.LblTip = new System.Windows.Forms.Label();
            this.PbxAbout = new System.Windows.Forms.PictureBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TxbMessagePattern = new XBase.TelegramBot.SyntaxHighlightingTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.NudRequestTimeoutMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(216, 246);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 8;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(297, 246);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // CbxConnectionManagers
            // 
            this.CbxConnectionManagers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CbxConnectionManagers.FormattingEnabled = true;
            this.CbxConnectionManagers.Location = new System.Drawing.Point(130, 12);
            this.CbxConnectionManagers.Name = "CbxConnectionManagers";
            this.CbxConnectionManagers.Size = new System.Drawing.Size(242, 21);
            this.CbxConnectionManagers.TabIndex = 1;
            // 
            // NudRequestTimeoutMS
            // 
            this.NudRequestTimeoutMS.Location = new System.Drawing.Point(130, 39);
            this.NudRequestTimeoutMS.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NudRequestTimeoutMS.Name = "NudRequestTimeoutMS";
            this.NudRequestTimeoutMS.Size = new System.Drawing.Size(120, 20);
            this.NudRequestTimeoutMS.TabIndex = 3;
            // 
            // LblConnectionManager
            // 
            this.LblConnectionManager.AutoSize = true;
            this.LblConnectionManager.Location = new System.Drawing.Point(12, 15);
            this.LblConnectionManager.Name = "LblConnectionManager";
            this.LblConnectionManager.Size = new System.Drawing.Size(108, 13);
            this.LblConnectionManager.TabIndex = 0;
            this.LblConnectionManager.Text = "Connection manager:";
            // 
            // LblRequestTimeoutMS
            // 
            this.LblRequestTimeoutMS.AutoSize = true;
            this.LblRequestTimeoutMS.Location = new System.Drawing.Point(12, 41);
            this.LblRequestTimeoutMS.Name = "LblRequestTimeoutMS";
            this.LblRequestTimeoutMS.Size = new System.Drawing.Size(106, 13);
            this.LblRequestTimeoutMS.TabIndex = 2;
            this.LblRequestTimeoutMS.Text = "Request timeout, ms:";
            // 
            // LblZeroMeansInfinite
            // 
            this.LblZeroMeansInfinite.AutoSize = true;
            this.LblZeroMeansInfinite.Location = new System.Drawing.Point(256, 41);
            this.LblZeroMeansInfinite.Name = "LblZeroMeansInfinite";
            this.LblZeroMeansInfinite.Size = new System.Drawing.Size(86, 13);
            this.LblZeroMeansInfinite.TabIndex = 4;
            this.LblZeroMeansInfinite.Text = "(0 means infinite)";
            // 
            // LblMessagePattern
            // 
            this.LblMessagePattern.AutoSize = true;
            this.LblMessagePattern.Location = new System.Drawing.Point(12, 66);
            this.LblMessagePattern.Name = "LblMessagePattern";
            this.LblMessagePattern.Size = new System.Drawing.Size(89, 13);
            this.LblMessagePattern.TabIndex = 5;
            this.LblMessagePattern.Text = "Message pattern:";
            // 
            // LblTip
            // 
            this.LblTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTip.BackColor = System.Drawing.SystemColors.Info;
            this.LblTip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblTip.Location = new System.Drawing.Point(12, 204);
            this.LblTip.Name = "LblTip";
            this.LblTip.Size = new System.Drawing.Size(360, 33);
            this.LblTip.TabIndex = 7;
            this.LblTip.Text = "Tip: Message pattern can contains system or user variables like @[System::Package" +
    "Name] or @[User::MyVariable]";
            // 
            // PbxAbout
            // 
            this.PbxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PbxAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxAbout.Image = global::XBase.TelegramBot.Properties.Resources.Help;
            this.PbxAbout.Location = new System.Drawing.Point(12, 253);
            this.PbxAbout.Margin = new System.Windows.Forms.Padding(0);
            this.PbxAbout.Name = "PbxAbout";
            this.PbxAbout.Size = new System.Drawing.Size(16, 16);
            this.PbxAbout.TabIndex = 10;
            this.PbxAbout.TabStop = false;
            this.ToolTip.SetToolTip(this.PbxAbout, "About...");
            this.PbxAbout.Click += new System.EventHandler(this.PbxAbout_Click);
            // 
            // TxbMessagePattern
            // 
            this.TxbMessagePattern.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbMessagePattern.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxbMessagePattern.Location = new System.Drawing.Point(12, 82);
            this.TxbMessagePattern.Name = "TxbMessagePattern";
            this.TxbMessagePattern.Size = new System.Drawing.Size(360, 119);
            this.TxbMessagePattern.TabIndex = 6;
            this.TxbMessagePattern.Text = "";
            // 
            // TelegramBotTaskUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(384, 281);
            this.Controls.Add(this.PbxAbout);
            this.Controls.Add(this.LblTip);
            this.Controls.Add(this.LblMessagePattern);
            this.Controls.Add(this.LblZeroMeansInfinite);
            this.Controls.Add(this.LblRequestTimeoutMS);
            this.Controls.Add(this.LblConnectionManager);
            this.Controls.Add(this.NudRequestTimeoutMS);
            this.Controls.Add(this.CbxConnectionManagers);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.TxbMessagePattern);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 320);
            this.Name = "TelegramBotTaskUIForm";
            this.Text = "Telegram Bot Task";
            ((System.ComponentModel.ISupportInitialize)(this.NudRequestTimeoutMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal SyntaxHighlightingTextBox TxbMessagePattern;
        internal System.Windows.Forms.Button BtnOK;
        internal System.Windows.Forms.Button BtnCancel;
        internal System.Windows.Forms.ComboBox CbxConnectionManagers;
        internal System.Windows.Forms.NumericUpDown NudRequestTimeoutMS;
        private System.Windows.Forms.Label LblConnectionManager;
        private System.Windows.Forms.Label LblRequestTimeoutMS;
        private System.Windows.Forms.Label LblZeroMeansInfinite;
        private System.Windows.Forms.Label LblMessagePattern;
        private System.Windows.Forms.Label LblTip;
        private System.Windows.Forms.PictureBox PbxAbout;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}