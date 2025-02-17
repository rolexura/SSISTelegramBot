using System;

namespace XBase.TelegramBot {
    public class TelegramBotTaskModel {
        public string ConnectionManagerUUID { get; set; } = string.Empty;
        public string MessagePattern { get; set; } = string.Empty;
        public int RequestTimeoutMS { get; set; } = 1000;
    }
}