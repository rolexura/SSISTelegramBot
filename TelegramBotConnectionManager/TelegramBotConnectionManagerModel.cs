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