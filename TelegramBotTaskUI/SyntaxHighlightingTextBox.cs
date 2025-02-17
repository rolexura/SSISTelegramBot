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