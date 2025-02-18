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
using System.ComponentModel;
using System.IO;
using System.Xml;
using Microsoft.SqlServer.Dts.Runtime;
using DTSExecResult = Microsoft.SqlServer.Dts.Runtime.DTSExecResult;

namespace XBase.TelegramBot {
    [DtsConnection(
        ConnectionType = "TGBOT",
        DisplayName = "Telegram Bot Connection Manager",
        Description = "Manages connections for a Telegram bot",
        UITypeName =
            "XBase.TelegramBot.TelegramBotConnectionManagerUI,XBase.TelegramBotConnectionManager.UI,Version=1.0.0.0," +
            "Culture=neutral,PublicKeyToken=54ddf9908a8304bd"
            //IconResource = "XBase.TelegramBot.TelegramBotConnectionManager.ico" // Does not work due to bug in VS2019 and/or SSIS 2017
    )]

    public class TelegramBotConnectionManager : SensitiveConnectionManagerBase {
        [DtsProperty, Browsable(false)]
        public TelegramBotConnectionManagerModel Model { get; } = new TelegramBotConnectionManagerModel();

        [DtsProperty, SensitiveString(true), Browsable(false)]
        public string ConnectionParameters {
            get {
                using (var stringWriter = new StringWriter()) {
                    using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true })) {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("ConnectionParameters");
                        writer.WriteAttributeString("Version", TelegramBotConnectionManagerModel.CurrentVersion.ToString());
                        writer.WriteElementString("ChatID", Model.ChatID);
                        writer.WriteElementString("AccessToken", Model.AccessToken);
                        writer.WriteEndElement(); // End of ConnectionParameters
                        writer.WriteEndDocument();
                    }
                    return stringWriter.ToString();
                }
            }
            set {
                var firstElement = true;
                using (var reader = XmlReader.Create(new StringReader(value))) {
                    while (reader.Read()) {
                        if (firstElement) {
                            if (reader.NodeType == XmlNodeType.XmlDeclaration) {
                                continue; // Impossible, but just in case
                            }
                            if (reader.NodeType != XmlNodeType.Element || reader.Name != "ConnectionParameters"
                                    || !reader.HasAttributes) {
                                break;
                            }
                            var versionString = reader.GetAttribute("Version");
                            if (!int.TryParse(versionString, out var version) || version <= 0) {
                                break;
                            }
                            Model.Version = version;
                            firstElement = false;
                            continue;
                        }

                        if (reader.NodeType == XmlNodeType.Element) {
                            switch (reader.Name) {
                                case "ChatID":
                                    reader.Read();
                                    Model.ChatID = reader.Value;
                                    break;
                                case "AccessToken":
                                    reader.Read();
                                    Model.AccessToken = reader.Value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static explicit operator TelegramBotConnectionManager(ConnectionManager v) {
            throw new System.NotImplementedException();
        }

        [DtsProperty]
        public string ChatID {
            get => Model.ChatID;
            set => Model.ChatID = value;
        }

        [DtsProperty, PasswordPropertyText(true)]
        public string AccessToken {
            get => Model.AccessToken;
            set => Model.AccessToken = value;
        }

        public override string ConnectionString {
            get => "";
            set { }
        }

        public override object AcquireConnection(object transaction) {
            return Model;
        }

        public override DTSExecResult Validate(IDTSInfoEvents infoEvents) {
            var chatIDNotSet = string.IsNullOrWhiteSpace(Model.ChatID);
            var accessTokenNotSet = string.IsNullOrWhiteSpace(Model.AccessToken);
            if (!chatIDNotSet && !accessTokenNotSet && Model.Version != 0) {
                return DTSExecResult.Success;
            }

            var errorMessage = Model.Version == 0
                ? "Error reading connection parameters"
                : $"{(chatIDNotSet ? "ChatID" : "Access token")} not set";
            infoEvents.FireError(0, "TelegramBotConnectionManager",
                errorMessage,  string.Empty, 0);
            return DTSExecResult.Failure;
        }
    }
}