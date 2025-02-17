using System;

namespace XBase.TelegramBot {

    public class TelegramBotConnectionManagerModel {
        public const int CurrentVersion = 1;

        private string _chatID = string.Empty;
        private string _accessToken = string.Empty;
        public int Version { get; set; } = 0;

        public string ChatID {
            get => _chatID;
            set {
                _chatID = value;
                OnChatIDChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string AccessToken {
            get => _accessToken;
            set {
                _accessToken = value;
                OnAccessTokenChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnChatIDChanged;
        public event EventHandler OnAccessTokenChanged;
    }
}