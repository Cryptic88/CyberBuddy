using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using POE_Cybersecurity_Chatbot;
using static POE_CyberBuddy.QuizManager;

namespace POE_CyberBuddy
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer reminderTimer;
        private Conversation chatbot = new Conversation();
        private ConversationPart2 conversationPart2;
        private InputProcessor inputProcessor;
        private ASCII_Effects asciiArt = new ASCII_Effects();
        private TipsManager tipManager;
        private ExplanationManager explanationManager = new ExplanationManager();
        private TaskManager taskManager = new TaskManager();
        private QuizManager quizManager = new QuizManager();
        private SentimentManager sentimentManager = new SentimentManager();
        private QuizHandler quizHandler;
        private bool isInQuiz = false;
        private string userName = "";

        public Form1()
        {
            InitializeComponent();

            asciiArt = new ASCII_Effects();
            tipManager = new TipsManager();
            taskManager = new TaskManager();        
            conversationPart2 = new ConversationPart2(asciiArt, tipManager, taskManager);
            sentimentManager = new SentimentManager();
            explanationManager = new ExplanationManager();
            chatbot = new Conversation();
            quizManager = new QuizManager();
            quizHandler = new QuizHandler(
                quizManager,
                text =>
                {
                    if (rtbOutput.InvokeRequired)
                    {
                        rtbOutput.Invoke(new Action(() => {
                        rtbOutput.AppendText(text);
                        ScrollToBottom();
                        }));
                    }
                    else
                    {
                        rtbOutput.AppendText(text);
                        ScrollToBottom();
                    }
                },                  // UI Append Text
                async (text) => await asciiArt.SpeakAsync(text),      // Text-to-speech async
                ScrollToBottom,                                       // Scroll UI
                log => conversationPart2.LogAction(log)
            ); // (Microsoft Learn, 2024)

            // Instantiate InputProcessor with dependencies and UI callbacks
            inputProcessor = new InputProcessor(
                conversationPart2,
                taskManager,
                sentimentManager,
                quizHandler,
                explanationManager,
                chatbot,
                asciiArt,
                userName,
                text => rtbOutput.AppendText(text),
                () => ScrollToBottom(),
                () => txtInput.Clear(),
                () => isInQuiz,
                input => quizHandler.HandleResponse(input),
                (input, user) => conversationPart2.HandleNLPInput(input, user)
            ); // (Microsoft Learn, 2024)

            // Event connections
            btnSend.Click += BtnSend_Click;
            btnCommands.Click += BtnCommands_Click;
            txtInput.KeyDown += TxtInput_KeyDown;

            // Clear placeholder text when clicked
            txtInput.Click += (s, e) =>
            {
                if (txtInput.Text == "Type your message here")
                    txtInput.Clear();
            };
        }
        


        private async void Form1_Load(object sender, EventArgs e)
        {

            using (var prompt = new UsernamePrompt())
            {
                if (prompt.ShowDialog() == DialogResult.OK) //(Microsoft Learn, 2025)
                {
                    userName = prompt.UserName;
                }
                else
                {
                    MessageBox.Show("Username is required to use Cyber Buddy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }

            // Set username in the label
            lblLoggedIn.Text = $"Logged in as: {userName}";

            // Initialize chatbot with username
            chatbot.Initialize(userName);

            // Play sound synchronously
            SoundPlayer player = new SoundPlayer("Recording.wav");
            player.Play();

            Font oldFont = rtbOutput.Font; // (Microsoft learn, 2025b)

            rtbOutput.SelectionFont = new Font("Consolas", 8F, FontStyle.Regular);
            rtbOutput.AppendText(asciiArt.ShowAsciiArt() + Environment.NewLine);

            rtbOutput.SelectionFont = oldFont;

            rtbOutput.AppendText(chatbot.ShowWelcomeMessage(userName) + Environment.NewLine);
            ScrollToBottom();

            reminderTimer = new System.Windows.Forms.Timer();
            reminderTimer.Interval = 60000; // Check every 60 seconds
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            await inputProcessor.ProcessInput(txtInput.Text);
            ScrollToBottom();
        }

        private void BtnCommands_Click(object sender, EventArgs e)
        {
            // Get the command list
            string commands = chatbot.GetCommandList();
            rtbOutput.AppendText(commands + Environment.NewLine + Environment.NewLine);
            ScrollToBottom();
        }

        private async void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter key sends the message
            {
                
                e.SuppressKeyPress = true; // Avoid beep sound
                await inputProcessor.ProcessInput(txtInput.Text);
                txtInput.Focus();
            }
        }


        private void ScrollToBottom()
        {
            rtbOutput.SelectionStart = rtbOutput.Text.Length;
            rtbOutput.ScrollToCaret();
            rtbOutput.Focus();
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            using (var addTaskForm = new AddTaskForm())
            {
                if (addTaskForm.ShowDialog() == DialogResult.OK) //(Microsoft Learn, 2025)
                {
                    // Get title and description
                    string title = addTaskForm.TaskTitle.Trim();
                    string description = addTaskForm.TaskDescription.Trim();

                    if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
                    {
                        // Ask if user wants a reminder
                        var result = MessageBox.Show($"Task \"{title}\" added!\nWould you like to set a reminder?",
                                                      "Add Reminder?",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            using (var reminderForm = new SetReminderForm())
                            {
                                if (reminderForm.ShowDialog() == DialogResult.OK && reminderForm.Days > 0) //(Microsoft Learn, 2025)
                                {
                                    // Add task with reminder
                                    taskManager.AddTask(title, description, DateTime.Now.AddDays(reminderForm.Days));
                                    conversationPart2.LogAction($"User added task: \"{title}\" with reminder in {reminderForm.Days} days.");
                                    var confirmMsg = $"Cyber Buddy: Task \"{title}\" added with a reminder in {reminderForm.Days} days!\n";
                                    rtbOutput.AppendText(confirmMsg);
                                    asciiArt.Speak(confirmMsg);
                                }
                                else
                                {
                                    // User cancelled or entered invalid days
                                    taskManager.AddTask(title, description);
                                    conversationPart2.LogAction($"User added task: \"{title}\" without a reminder (reminder cancelled or invalid input).");
                                    var noReminderMsg = $"Cyber Buddy: Task \"{title}\" added with no reminder.\n";
                                    rtbOutput.AppendText(noReminderMsg + Environment.NewLine);
                                    asciiArt.Speak(noReminderMsg);
                                }
                            }
                        }
                        else
                        {
                            // User doesn’t want a reminder
                            taskManager.AddTask(title, description);
                            conversationPart2.LogAction($"User added task: \"{title}\" without a reminder (user declined).");
                            var noReminderMsg = $"Cyber Buddy: Task \"{title}\" added with no reminder.\n";
                            rtbOutput.AppendText(noReminderMsg + Environment.NewLine);
                            asciiArt.Speak(noReminderMsg);
                        }

                        // Final instructions
                        var taskInstructions =
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
        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            var tasks = taskManager.GetAllTasks();
            var dueTasks = tasks
                .Where(t => !t.IsCompleted && t.ReminderDate.HasValue && t.ReminderDate.Value.Date <= DateTime.Now.Date)
                .ToList();

            foreach (var dueTask in dueTasks)
            {
                // Show alert
                MessageBox.Show($"Reminder!\nTask \"{dueTask.Title}\" is due.\n{dueTask.Description}",
                                "Task Reminder",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        private async void btnStartQuiz_Click(object sender, EventArgs e)
        {
            await quizHandler.StartQuiz(userName);
            ScrollToBottom();
        }

        private void btnActivityLog_Click(object sender, EventArgs e)
        {
            var log = conversationPart2.ShowActivityLog();
            rtbOutput.AppendText(log + Environment.NewLine);
            ScrollToBottom();
        }
    }
}

/*Refrence List:
 
    Microsoft Learn, 2024. .NET dependency injection. [online] Available at: 
    https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
    (Accessed: 25 June 2025).

    Microsoft Learn, 2025. Form.ShowDialog Method. [online] Available at: 
    https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form.showdialog?view=windowsdesktop-9.0
    (Accessed: 23 June 2025).

    Microsoft Learn, 2025b. RichTextBox Class. [online] Available at:
    https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.richtextbox?view=windowsdesktop-9.0
    (Accessed 22 June 2025).

    Microsoft Learn, 2025c. How to: Run Procedures at Set Intervals with the Windows Forms Timer Component. [online] Available at:
    https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/run-procedures-at-set-intervals-with-wf-timer-component
    (Accessed 26 June 2025).
 */

