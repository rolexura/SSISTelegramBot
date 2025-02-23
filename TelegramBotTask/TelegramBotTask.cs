/*
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
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.SqlServer.Dts.Runtime;

namespace XBase.TelegramBot {
    [DtsTask(
        DisplayName = "Telegram Bot Task",
        Description = "A SSIS task to send messages to Telegram",
        TaskContact = "Rostislav Uralskyi <rolex@ukr.net>",
        RequiredProductLevel = DTSProductLevel.None,
        UITypeName = "XBase.TelegramBot.TelegramBotTaskUI,XBase.TelegramBotTask.UI," +
#if SQL2017
            "Version=1.0.0.2017," +
#elif SQL2019
            "Version=1.0.0.2019," +
#elif SQL2022
            "Version=1.0.0.2022," +
#else
#error "This code must be compiled with SQL2017, SQL2019 or SQL2022 defined."
#endif
                     "Culture=neutral,PublicKeyToken=54ddf9908a8304bd",
        IconResource = "XBase.TelegramBot.TelegramBotTask.ico"
    )]
    public class TelegramBotTask : Task, IDTSComponentPersist {
        [DtsProperty, Browsable(false)]
        public TelegramBotTaskModel Model { get; } = new TelegramBotTaskModel();

        [DtsProperty, Browsable(false)]
        public string ConnectionManagerUUID {
            get => Model.ConnectionManagerUUID;
            set => Model.ConnectionManagerUUID = value;
        }

        [DtsProperty, Browsable(false)]
        public string MessagePattern {
            get => Model.MessagePattern;
            set => Model.MessagePattern = value;
        }

        [DtsProperty]
        public int RequestTimeoutMS {
            get => Model.RequestTimeoutMS;
            set => Model.RequestTimeoutMS = value;
        }

        private ConnectionManager _connMgr;
        private readonly Regex _patternRegex = new Regex(@"@\[(?<ns>[A-Za-z_][A-Za-z0-9_]*)::(?<name>[A-Za-z_][A-Za-z0-9_]*)\]",
                RegexOptions.Compiled);

        public override DTSExecResult Execute(Connections connections, VariableDispenser variableDispenser,
                    IDTSComponentEvents componentEvents, IDTSLogging log, object transaction) {
            var tmpConnMgrModel = _connMgr.AcquireConnection(null);
            if (!(tmpConnMgrModel is TelegramBotConnectionManagerModel tgBotConnMgrModel)) {
                componentEvents.FireError(0, "Telegram Bot Task", "Selected connection manager has invalid type",
                    string.Empty, 0);
                return DTSExecResult.Failure;
            }

            var fireAgain = false;
            componentEvents.FireInformation(0, "Telegram Bot Task", $"Using connection manager: {_connMgr.Name}",
                string.Empty, 0, ref fireAgain);

            var message = GetMessage(variableDispenser);
            //componentEvents.FireInformation(0, "Telegram Bot Task", message,
            //    string.Empty, 0, ref fireAgain);

            // Use the connection manager for operations here
            // Enable TLS 1.2 or later
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            // Construct the URL
            var url = $"https://api.telegram.org/bot{tgBotConnMgrModel.AccessToken}/sendMessage?chat_id={tgBotConnMgrModel.ChatID}&text={message}";

            // Create the WebRequest
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = RequestTimeoutMS <= 0 ? System.Threading.Timeout.Infinite : RequestTimeoutMS;

            try {
                // Get the response
                using (var response = request.GetResponse()) {
                    using (var stream = response.GetResponseStream()) {
                        if (stream == null) {
                            return DTSExecResult.Failure;
                        }
                    }
                }
            } catch (WebException) {
                return DTSExecResult.Failure;
            }
            return DTSExecResult.Success;
        }

        public override DTSExecResult Validate(Connections connections, VariableDispenser variableDispenser,
                    IDTSComponentEvents componentEvents, IDTSLogging log) {
            var patternIsEmpty = string.IsNullOrWhiteSpace(Model.MessagePattern);
            var connMgr = GetConnectionManagerByUUID(connections, Model.ConnectionManagerUUID);
            if (connMgr != null && !patternIsEmpty) {
                _connMgr = connMgr;
                return DTSExecResult.Success;
            }
            componentEvents.FireError(0, "Telegram Bot Task", patternIsEmpty ?
                    "Message pattern is not set" : "No connection manager selected",
                string.Empty, 0);
            return DTSExecResult.Failure;
        }

        private string GetMessage(VariableDispenser variableDispenser) {
            var matches = _patternRegex.Matches(MessagePattern);
            Variables variables = null;
            foreach (Match match in matches) {
                var qualifiedName = $"{match.Groups["ns"].Value}::{match.Groups["name"].Value}";
                if (variableDispenser.Contains(qualifiedName)) {
                    variableDispenser.LockForRead(qualifiedName);
                }
            }
            variableDispenser.GetVariables(ref variables);
            foreach (var variable in variables) {
                MessagePattern = MessagePattern.Replace($"@[{variable.QualifiedName}]", variable.Value.ToString());
            }
            variables.Unlock();

            return MessagePattern;
        }

        #region Implementation of IDTSComponentPersist
        public void SaveToXML(XmlDocument doc, IDTSInfoEvents infoEvents) {
            var rootElement = doc.DocumentElement;
            if (rootElement == null) {
                rootElement = doc.CreateElement("DTSObject");
                doc.AppendChild(rootElement);
            }
            var configElement = doc.CreateElement("ConnectionManagerUUID");
            configElement.InnerText = ConnectionManagerUUID;
            rootElement.AppendChild(configElement);

            configElement = doc.CreateElement("MessagePattern");
            configElement.InnerText = MessagePattern;
            rootElement.AppendChild(configElement);
            
            configElement = doc.CreateElement("RequestTimeoutMS");
            configElement.InnerText = RequestTimeoutMS.ToString();
            rootElement.AppendChild(configElement);
       }

        public void LoadFromXML(XmlElement element, IDTSInfoEvents infoEvents) {
            var node = element.SelectSingleNode("ConnectionManagerUUID");
            if (node == null) {
                return;
            }
            ConnectionManagerUUID = node.InnerText;

            node = element.SelectSingleNode("MessagePattern");
            if (node == null) {
                return;
            }
            MessagePattern = node.InnerText;

            node = element.SelectSingleNode("RequestTimeoutMS");
            if (node != null && int.TryParse(node.InnerText, out var requestTimeoutMS)) {
                RequestTimeoutMS = requestTimeoutMS;
            }
        }
        #endregion

        private static ConnectionManager GetConnectionManagerByUUID(Connections connections, string uuid) {
            if (string.IsNullOrEmpty(uuid)) {
                return null;
            }
            foreach (var connection in connections) {
                if (connection.CreationName == "TGBOT" && connection.ID == uuid) {
                    return connection;
                }
            }
            return null;
        }
    }
}