﻿/*
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
using System.Reflection;
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