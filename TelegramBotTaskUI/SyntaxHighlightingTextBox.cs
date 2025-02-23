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
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace XBase.TelegramBot {
    public class SyntaxHighlightingTextBox : RichTextBox {
        private readonly Regex _patternRegex;

        public SyntaxHighlightingTextBox() {
            // Define the regex pattern for text between @[ and ]
            _patternRegex = new Regex(@"@\[[A-Za-z_][A-Za-z0-9_]*::[A-Za-z_][A-Za-z0-9_]*\]", RegexOptions.Compiled);
            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e) {
            HighlightMatches();
        }

        internal void HighlightMatches() {
            var originalSelectionStart = SelectionStart;
            var originalSelectionLength = SelectionLength;

            TextChanged -= OnTextChanged; // Temporarily unsubscribe to avoid recursion

            // Clear formatting
            SelectAll();
            SelectionColor = Color.Black;

            foreach (Match match in _patternRegex.Matches(Text)) {
                Select(match.Index, match.Length);
                SelectionColor = Color.DarkGreen;
            }

            // Restore original selection
            SelectionStart = originalSelectionStart;
            SelectionLength = originalSelectionLength;
            SelectionColor = Color.Black; // Reset the color to default

            TextChanged += OnTextChanged; // Resubscribe
        }
    }
}