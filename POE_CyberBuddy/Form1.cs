using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using POE_Cybersecurity_Chatbot;

namespace POE_CyberBuddy
{
    public partial class Form1 : Form
    {
        private Conversation chatbot = new Conversation();
        private ConversationPart2 conversationPart2;
        private ASCII_Effects asciiArt = new ASCII_Effects();
        private TipsManager tipManager;
        private TaskManager taskManager = new TaskManager();
        private QuizManager quizManager = new QuizManager();
        private bool isInQuiz = false;
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
            tipManager = new TipsManager();
            conversationPart2 = new ConversationPart2(asciiArt, tipManager, taskManager);
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

            if (isInQuiz)
            {
                HandleQuizResponse(input);
                return;
            }

            if (input.Equals("show more", StringComparison.OrdinalIgnoreCase))
            {
                var reply = conversationPart2.ShowActivityLog(showMore: true);
                rtbOutput.AppendText($"You: {input}\n{reply}\n");
                asciiArt.Speak("Here are more activities from your log.");
                ScrollToBottom();
                txtInput.Clear();
                return;
            }

            if (input.Equals("show activity log", StringComparison.OrdinalIgnoreCase) ||
            input.Equals("what have you done", StringComparison.OrdinalIgnoreCase))
            {
                var reply = conversationPart2.ShowActivityLog();
                rtbOutput.AppendText($"You: {input}\n{reply}\n");
                asciiArt.Speak("Here’s a summary of recent activity."); // Speak only the summary
                ScrollToBottom();
                txtInput.Clear();
                return;
            }

            if (input.Equals("list tasks", StringComparison.OrdinalIgnoreCase))
            {
                conversationPart2.LogAction("User requested to list all tasks.");

                var allTasks = taskManager.GetAllTasks();
                if (allTasks.Count == 0)
                {
                    string reply = "Cyber Buddy: You have no tasks.";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else
                {
                    string replyHeader = "Cyber Buddy: Here are your tasks:";
                    rtbOutput.AppendText(replyHeader + Environment.NewLine);
                    asciiArt.Speak(replyHeader);
                    foreach (var t in allTasks)
                    {
                        rtbOutput.AppendText(t.ToString() + Environment.NewLine);
                        asciiArt.Speak(t.ToString());
                    }
                }
                ScrollToBottom();
                txtInput.Clear();
                return;
            }

            if (input.StartsWith("complete task", StringComparison.OrdinalIgnoreCase))
            {
                var title = input.Substring("complete task".Length).Trim();
                if (string.IsNullOrEmpty(title))
                {
                    string reply = "Cyber Buddy: Please provide the title of the task you want to complete.";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else if (taskManager.CompleteTask(title))
                {
                    conversationPart2.LogAction($"User completed task: \"{title}\"");
                    string reply = $"Cyber Buddy: Marked task \"{title}\" as completed.";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else
                {
                    conversationPart2.LogAction($"User tried to complete a non-existing task: \"{title}\"");
                    string reply = $"Cyber Buddy: Couldn't find a task named \"{title}\".";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                ScrollToBottom();
                txtInput.Clear();
                return;
            }

            if (input.StartsWith("delete task", StringComparison.OrdinalIgnoreCase))
            {
                var title = input.Substring("delete task".Length).Trim();
                if (string.IsNullOrEmpty(title))
                {
                    string reply = "Cyber Buddy: Please provide the title of the task you want to delete.";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else if (taskManager.DeleteTask(title))
                {
                    conversationPart2.LogAction($"User deleted task: \"{title}\"");
                    string reply = $"Cyber Buddy: Deleted Task: \"{title}\".";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else
                {
                    conversationPart2.LogAction($"User tried to delete a non-existing task: \"{title}\"");
                    string reply = $"Cyber Buddy: Couldn't find a task named \"{title}\".";
                    rtbOutput.AppendText(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                ScrollToBottom();
                txtInput.Clear();
                return;
            }
            var nlpResponse = conversationPart2.HandleNLPInput(input, userName);
            if (!string.IsNullOrEmpty(nlpResponse))
            {
                rtbOutput.AppendText($"You: {input}\n");
                asciiArt.Speak(nlpResponse);
                rtbOutput.AppendText(nlpResponse + Environment.NewLine);
                ScrollToBottom();
                txtInput.Clear();
                return;
            }

            // Get bot response
            string botResponse = chatbot.StartConversation(input, userName);

            if (!string.IsNullOrEmpty(botResponse))
            {
                // Check if the bot response contains a tip keyword

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

                Label textLabel = new Label() { Left = 20, Top = 20, Text = "Please enter your username:", AutoSize = true };
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
        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 400;
                prompt.Height = 250;
                prompt.Text = "Add New Task";

                Label titleLabel = new Label() { Left = 20, Top = 20, Text = "Task Title:", AutoSize = true };
                TextBox titleBox = new TextBox() { Left = 20, Top = 50, Width = 300 };
                Label descriptionLabel = new Label() { Left = 20, Top = 90, Text = "Task Description:", AutoSize = true };
                TextBox descriptionBox = new TextBox() { Left = 20, Top = 120, Width = 300 };
                Button confirmation = new Button() { Text = "OK", Left = 20, Width = 100, Top = 160 };
                confirmation.DialogResult = DialogResult.OK;

                prompt.Controls.Add(titleLabel);
                prompt.Controls.Add(titleBox);
                prompt.Controls.Add(descriptionLabel);
                prompt.Controls.Add(descriptionBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    string title = titleBox.Text.Trim();
                    string description = descriptionBox.Text.Trim();

                    if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
                    {
                        // STEP 2: Ask if user wants a reminder
                        var result = MessageBox.Show($"Task \"{title}\" added!\nWould you like to set a reminder?", "Add Reminder?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            int days = 0;
                            bool validInput = false;

                            while (!validInput)
                            {
                                using (Form reminderPrompt = new Form())
                                {
                                    reminderPrompt.Width = 350;
                                    reminderPrompt.Height = 180;
                                    reminderPrompt.Text = "Set Reminder";

                                    Label reminderLabel = new Label() { Left = 20, Top = 20, AutoSize = true, Text = "Remind me in how many days?" };
                                    TextBox reminderBox = new TextBox() { Left = 20, Top = 50, Width = 100 };
                                    Button setButton = new Button() { Text = "Set", Left = 130, Width = 80, Top = 48 };
                                    setButton.DialogResult = DialogResult.OK;

                                    reminderPrompt.Controls.Add(reminderLabel);
                                    reminderPrompt.Controls.Add(reminderBox);
                                    reminderPrompt.Controls.Add(setButton);
                                    reminderPrompt.AcceptButton = setButton;

                                    var reminderResult = reminderPrompt.ShowDialog();

                                    if (reminderResult == DialogResult.OK)
                                    {
                                        string input = reminderBox.Text.Trim();

                                        if (int.TryParse(input, out days) && days > 0)
                                        {
                                            validInput = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please enter a valid number of days (e.g., 5) without extra words.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        // User cancelled the reminder input prompt
                                        break;
                                    }
                                }
                            }

                            if (validInput)
                            {
                                // Add task with reminder
                                taskManager.AddTask(title, description, DateTime.Now.AddDays(days));
                                conversationPart2.LogAction($"User added task: \"{title}\" with reminder set in {days} days.");
                                string confirmMsg = $"Cyber Buddy: Task \"{title}\" added with a reminder in {days} days!\n";
                                rtbOutput.AppendText(confirmMsg);
                                asciiArt.Speak(confirmMsg);
                            }
                            else
                            {
                                // User cancelled or no valid input, add without reminder
                                taskManager.AddTask(title, description);
                                conversationPart2.LogAction($"User added task: \"{title}\" without a reminder (reminder cancelled or invalid input).");
                                string confirmNoReminderMsg = $"Cyber Buddy: Task \"{title}\" added with no reminder.\n";
                                rtbOutput.AppendText(confirmNoReminderMsg + Environment.NewLine);
                                asciiArt.Speak(confirmNoReminderMsg);
                            }
                        }
                        else
                        {
                            // User doesn’t want a reminder
                            taskManager.AddTask(title, description);
                            conversationPart2.LogAction($"User added task: \"{title}\" without a reminder (user declined).");
                            string noReminderMsg = $"Cyber Buddy: Task \"{title}\" added with no reminder.\n";
                            rtbOutput.AppendText(noReminderMsg + Environment.NewLine);
                            asciiArt.Speak(noReminderMsg);
                        }
                        string taskInstructions =
                            "Cyber Buddy: To manage your tasks, you can type commands like:\n" +
                            " - list tasks\n" +
                            " - complete task [task title]\n" +
                            " - delete task [task title]\n\n";

                        rtbOutput.AppendText(taskInstructions);
                        asciiArt.Speak(taskInstructions);
                    }
                    else
                    {
                        MessageBox.Show("Please provide both a title and description.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            isInQuiz = true;
            quizManager.Reset();
            conversationPart2.LogAction("Quiz started by user.");
            AskNextQuestion();
        }
        private void AskNextQuestion()
        {
            if (quizManager.HasMoreQuestions())
            {
                var q = quizManager.GetCurrentQuestion();
                rtbOutput.AppendText($"Cyber Buddy Quiz:\n{q.Text}\n");
                asciiArt.Speak($"Question {quizManager.GetCurrentQuestionNumber()}: {q.Text}");
                for (int i = 0; i < q.Options.Count; i++)
                {
                    rtbOutput.AppendText($"{q.Options[i]}\n");
                }
                ScrollToBottom();

                conversationPart2.LogAction($"Question {quizManager.GetCurrentQuestionNumber()} displayed: \"{q.Text}\"");
            }
            else
            {
                EndQuiz();
            }
        }

        private void HandleQuizResponse(string input)
        {
            // Expecting the user to reply with the option number or letter
            int userChoice = -1;

            // Try extracting number from user input
            if (int.TryParse(input, out int parsedNumber))
            {
                userChoice = parsedNumber - 1;
            }
            else
            {
                // Try extracting A/B/C/D
                var upperInput = input.ToUpper();
                if (upperInput.Length == 1 && upperInput[0] >= 'A' && upperInput[0] <= 'D')
                {
                    userChoice = upperInput[0] - 'A';
                }
            }

            if (userChoice != -1)
            {
                bool isCorrect = quizManager.CheckAnswer(userChoice, out string explanation);
                conversationPart2.LogAction($"User answered question {quizManager.GetCurrentQuestionNumber()} with option {(char)('A' + userChoice)} - {(isCorrect ? "Correct" : "Incorrect")}");

                if (isCorrect)
                {
                    rtbOutput.AppendText($"Cyber Buddy: Correct! {explanation}");
                    asciiArt.Speak($"Correct! {explanation}");
                    rtbOutput.AppendText("\n**************************************\n\n");
                }
                else
                {
                    rtbOutput.AppendText($"Cyber Buddy: Incorrect. {explanation}");
                    asciiArt.Speak($"Incorrect. {explanation}");
                    rtbOutput.AppendText("\n**************************************\n\n");
                }

                quizManager.MoveToNextQuestion();

                if (quizManager.HasMoreQuestions())
                {
                    AskNextQuestion();
                }
                else
                {
                    EndQuiz();
                }
            }
            else
            {
                rtbOutput.AppendText("Cyber Buddy: Please enter a valid option (e.g., A, B, C, D or 1, 2, 3, 4)!\n");
                asciiArt.Speak("Please enter a valid option.");
            }

            ScrollToBottom();
            txtInput.Clear();
            txtInput.Focus();
        }

        private void EndQuiz()
        {
            isInQuiz = false;
            int finalScore = quizManager.GetFinalScore();
            var totalQuestions = quizManager.GetTotalQuestions();
            var finalFeedback = quizManager.GetFinalFeedback();
            var resultMessage = $"Cyber Buddy: Quiz finished!\nYour Score: {finalScore}/{totalQuestions}\n{finalFeedback}\n";
            rtbOutput.AppendText(resultMessage);
            asciiArt.Speak($"Quiz finished! You scored {finalScore} out of {totalQuestions}. {finalFeedback}");
            ScrollToBottom();

            conversationPart2.LogAction($"Quiz finished. Final score: {finalScore}/{totalQuestions}");
        }

        private void btnActivityLog_Click(object sender, EventArgs e)
        {
            var log = conversationPart2.ShowActivityLog();
            rtbOutput.AppendText(log + Environment.NewLine);
        }
    }
}

