using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Design;

namespace XBase.TelegramBot {
    public class TelegramBotConnectionManagerUI : IDtsConnectionManagerUI {
        private ConnectionManager _connectionManager;
        private TelegramBotConnectionManagerModel _model;
        private TelegramBotConnectionManagerUIForm _view;

        public void Initialize(ConnectionManager connectionManager, IServiceProvider serviceProvider) {
            _connectionManager = connectionManager;
            _model = (TelegramBotConnectionManagerModel)_connectionManager.Properties["Model"].GetValue(_connectionManager);
            _view = new TelegramBotConnectionManagerUIForm();

            // Subscribe to model events
            _model.OnChatIDChanged += Model_OnChatIDChanged;
            _model.OnAccessTokenChanged += Model_OnAccessTokenChanged;

            // Subscribe to user actions
            _view.BtnOK.Click += BtnOK_Click;
        }

        public bool New(IWin32Window parentWindow, Connections connections, ConnectionManagerUIArgs connectionUIArg) {
            _view.Text = _connectionManager.Name;
            _view.TxbChatID.Text = _model.ChatID;
            _view.TxbAccessToken.Text = _model.AccessToken;
            var res = _view.ShowDialog(parentWindow);
            return res == DialogResult.OK;
        }

        public bool Edit(IWin32Window parentWindow, Connections connections, ConnectionManagerUIArgs connectionUIArg) {
            return New(parentWindow, connections, connectionUIArg);
        }

        public void Delete(IWin32Window parentWindow) {
            //_view.Dispose();
        }

        private void Model_OnChatIDChanged(object sender, EventArgs e) {
            _view.TxbChatID.Text = _model.ChatID;
        }

        private void Model_OnAccessTokenChanged(object sender, EventArgs e) {
            _view.TxbAccessToken.Text = _model.AccessToken;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            _model.ChatID = _view.TxbChatID.Text;
            _model.AccessToken = _view.TxbAccessToken.Text;
        }
    }
}