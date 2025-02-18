/*
 * Copyright 2025 Rostislav Uralskyi
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 *     You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 *     distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *     See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Design;

namespace XBase.TelegramBot {
    public class ConnectionManagerUUIDKeyValuePair {
        private readonly string _name;

        public string UUID { get; }

        public ConnectionManagerUUIDKeyValuePair(string name, string uuid) {
            _name = name;
            UUID = uuid;
        }

        public override string ToString() {
            return _name;
        }
    }

    public class TelegramBotTaskUI : IDtsTaskUI {
        private TelegramBotTaskUIForm _view;
        private TaskHost _taskHost;
        private readonly List<ConnectionManagerUUIDKeyValuePair> _connectionManagersUUIDList = new List<ConnectionManagerUUIDKeyValuePair>();

        private TelegramBotTaskModel Model =>
            !(_taskHost.Properties["Model"].GetValue(_taskHost) is TelegramBotTaskModel model)
                ? null : model;

        public void Initialize(TaskHost taskHost, IServiceProvider serviceProvider) {
            _taskHost = taskHost;

            var package = _taskHost.Parent as Package;
            if (package == null) {
                MessageBox.Show("The parent container is not a Package. Cannot retrieve connections.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var connections = package.Connections;
            foreach (var connectionManager in connections) {
                if (connectionManager.CreationName != "TGBOT") {
                    continue;
                }

                var kvUUID = new ConnectionManagerUUIDKeyValuePair(connectionManager.Name, connectionManager.ID);
                _connectionManagersUUIDList.Add(kvUUID);
            }
        }

        public ContainerControl GetView() {
            _view = new TelegramBotTaskUIForm(_connectionManagersUUIDList);
            if (Model != null) {
                _view.TxbMessagePattern.Text = Model.MessagePattern;
                _view.NudRequestTimeoutMS.Value = Model.RequestTimeoutMS;
            }

            // Subscribe to user actions
            _view.BtnOK.Click += BtnOK_Click;
            _view.TxbMessagePattern.HighlightMatches();

            return _view;
        }

        public void New(IWin32Window parentWindow) {
            //_view = new TelegramBotTaskUIForm(_connectionManagersUUIDList);
            //_view.TxbChatID.Text = _model.ChatID;
            //_view.TxbMessagePattern.Text = _model.MessagePattern;

            // Subscribe to user actions
            //_view.BtnOK.Click += BtnOK_Click;
            //var res = _view.ShowDialog(parentWindow);
            //if (res == DialogResult.OK) {
            //}
        }

        public void Delete(IWin32Window parentWindow) {
            //_view.Dispose();
        }
        private void BtnOK_Click(object sender, EventArgs e) {
            if (Model == null) {
                return;
            }

            Model.ConnectionManagerUUID = ((ConnectionManagerUUIDKeyValuePair)_view.CbxConnectionManagers.SelectedItem).UUID;
            Model.MessagePattern = _view.TxbMessagePattern.Text;
            Model.RequestTimeoutMS = (int)_view.NudRequestTimeoutMS.Value;
        }
    }
}