using System;
using System.Windows.Forms;
using System.Media;
using System.Threading.Tasks;
using System.Threading;
using POE_Cybersecurity_Chatbot;

namespace POE_CyberBuddy
{
    public partial class Form1 : Form
    {
        private Conversation chatbot = new Conversation();
        private ASCII_Effects asciiArt = new ASCII_Effects();
        private string userName = "";

        public Form1()
        {
            InitializeComponent();

            // Event connections
            btnSend.Click += BtnSend_Click;
            btnCommands.Click += BtnCommands_Click;
            txtInput.KeyDown += TxtInput_KeyDown;

            // Optional: Clear placeholder text when clicked
            txtInput.Click += (s, e) =>
            {
                if (txtInput.Text == "Type your message here")
                    txtInput.Clear();
            };
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            userName = PromptForUsername();

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Username is required to use Cyber Buddy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Set username in the label
            lblLoggedIn.Text = $"Logged in as: {userName}";

            // Initialize chatbot with username
            chatbot.Initialize(userName);

            // Play sound synchronously
            SoundPlayer player = new SoundPlayer("Recording.wav");
            player.Play();

            Font oldFont = rtbOutput.Font;

            rtbOutput.SelectionFont = new Font("Consolas", 8F, FontStyle.Regular);
            rtbOutput.AppendText(asciiArt.ShowAsciiArt() + Environment.NewLine);

            rtbOutput.SelectionFont = oldFont;

            rtbOutput.AppendText(chatbot.ShowWelcomeMessage(userName) + Environment.NewLine);
            ScrollToBottom();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        private void BtnCommands_Click(object sender, EventArgs e)
        {
            // Get the command list
            string commands = chatbot.GetCommandList();
            rtbOutput.AppendText(commands + Environment.NewLine + Environment.NewLine);
            ScrollToBottom();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter key sends the message
            {
                ProcessInput();
                e.SuppressKeyPress = true; // Avoid beep sound
            }
        }

        private void ProcessInput()
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;


            // Get bot response
            string botResponse = chatbot.StartConversation(input, userName);

            if (!string.IsNullOrEmpty(botResponse))
            {
                // Check if the bot response contains a tip keyword
                // You can adjust these keywords based on your actual topics
                if (input.Contains("phishing", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("firewall", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("wifi", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("usb", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("social engineering", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("password", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("browse", StringComparison.OrdinalIgnoreCase))
                {
                    // Show relevant ASCII Art
                    AppendAsciiArt(asciiArt.ShowAsciiArtForTopic(input));
                }
                // Show user input
                rtbOutput.AppendText($"You: {input}\n" + Environment.NewLine);
                asciiArt.Speak(botResponse);
                rtbOutput.AppendText(botResponse + Environment.NewLine);
            }

            txtInput.Clear();
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            rtbOutput.SelectionStart = rtbOutput.Text.Length;
            rtbOutput.ScrollToCaret();
        }
        private void AppendAsciiArt(string asciiArtText)
        {
            // Save the current font
            var originalFont = rtbOutput.SelectionFont;

            // Switch to Consolas 8F
            rtbOutput.SelectionFont = new Font("Consolas", 8F, FontStyle.Regular);
            rtbOutput.AppendText(asciiArtText + Environment.NewLine);

            // Restore the original font
            rtbOutput.SelectionFont = originalFont;
        }
        private string PromptForUsername()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 400;
                prompt.Height = 180;
                prompt.Text = "Enter your username";

                Label textLabel = new Label() { Left = 20, Top = 20, Text = "Please enter your username:", AutoSize = true};
                TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 200 };
                Button confirmation = new Button() { Text = "OK", Left = 230, Width = 40, Top = 48, DialogResult = DialogResult.OK };

                int totalWidth = inputBox.Width + confirmation.Width + 10;

                // Left starting point for the TextBox
                int startLeft = (prompt.ClientSize.Width - totalWidth) / 2;

                // Position controls
                inputBox.Left = startLeft;
                confirmation.Left = inputBox.Right + 10;

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text.Trim() : null;
            }
        }
    }
}

