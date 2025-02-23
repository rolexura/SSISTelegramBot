
namespace XBase.TelegramBot {
    partial class TelegramBotConnectionManagerUIForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelegramBotConnectionManagerUIForm));
            this.LblChatID = new System.Windows.Forms.Label();
            this.TxbChatID = new System.Windows.Forms.TextBox();
            this.LblAccessToken = new System.Windows.Forms.Label();
            this.TxbAccessToken = new System.Windows.Forms.TextBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.PbxAbout = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PbxAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // LblChatID
            // 
            this.LblChatID.AutoSize = true;
            this.LblChatID.Location = new System.Drawing.Point(6, 15);
            this.LblChatID.Name = "LblChatID";
            this.LblChatID.Size = new System.Drawing.Size(46, 13);
            this.LblChatID.TabIndex = 0;
            this.LblChatID.Text = "Chat ID:";
            // 
            // TxbChatID
            // 
            this.TxbChatID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbChatID.Location = new System.Drawing.Point(93, 12);
            this.TxbChatID.Name = "TxbChatID";
            this.TxbChatID.Size = new System.Drawing.Size(302, 20);
            this.TxbChatID.TabIndex = 1;
            // 
            // LblAccessToken
            // 
            this.LblAccessToken.AutoSize = true;
            this.LblAccessToken.Location = new System.Drawing.Point(6, 42);
            this.LblAccessToken.Name = "LblAccessToken";
            this.LblAccessToken.Size = new System.Drawing.Size(75, 13);
            this.LblAccessToken.TabIndex = 2;
            this.LblAccessToken.Text = "Access token:";
            // 
            // TxbAccessToken
            // 
            this.TxbAccessToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxbAccessToken.Location = new System.Drawing.Point(93, 39);
            this.TxbAccessToken.Name = "TxbAccessToken";
            this.TxbAccessToken.Size = new System.Drawing.Size(302, 20);
            this.TxbAccessToken.TabIndex = 3;
            this.TxbAccessToken.UseSystemPasswordChar = true;
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(239, 72);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 4;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(320, 72);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PbxAbout
            // 
            this.PbxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PbxAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxAbout.Image = global::XBase.TelegramBot.Properties.Resources.Help;
            this.PbxAbout.Location = new System.Drawing.Point(9, 82);
            this.PbxAbout.Margin = new System.Windows.Forms.Padding(0);
            this.PbxAbout.Name = "PbxAbout";
            this.PbxAbout.Size = new System.Drawing.Size(16, 16);
            this.PbxAbout.TabIndex = 11;
            this.PbxAbout.TabStop = false;
            this.PbxAbout.Click += new System.EventHandler(this.PbxAbout_Click);
            // 
            // TelegramBotConnectionManagerUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 107);
            this.Controls.Add(this.PbxAbout);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.TxbAccessToken);
            this.Controls.Add(this.LblAccessToken);
            this.Controls.Add(this.TxbChatID);
            this.Controls.Add(this.LblChatID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TelegramBotConnectionManagerUIForm";
            this.Text = "Telegram Bot Connection Manager";
            ((System.ComponentModel.ISupportInitialize)(this.PbxAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TxbAccessToken;
        public System.Windows.Forms.TextBox TxbChatID;

        private System.Windows.Forms.Label LblChatID;
        private System.Windows.Forms.Label LblAccessToken;
        private System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.PictureBox PbxAbout;
    }
}