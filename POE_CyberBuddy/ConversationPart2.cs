using System;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using POE_CyberBuddy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace POE_Cybersecurity_Chatbot
{
    public class ConversationPart2
    {

        private TipsManager tipManager;
        private TaskManager taskManager;
        private ExplanationManager explanationManager;
        private SentimentManager sentimentManager;
        private Memory memory = new Memory();
        private string? lastAddedTaskTitle = null;
        private string currentTopic;
        private List<string> recentActions = new List<string>();
        private List<string> activityLog = new List<string>();
        public ConversationPart2(ASCII_Effects asciiArt, TipsManager tipManager, TaskManager taskManager)
        {
            
            this.tipManager = tipManager;
            this.taskManager = taskManager;
            this.sentimentManager = new SentimentManager();
            this.explanationManager = new ExplanationManager();
        }


        // Handles keyword-based responses. Returns true if a keyword matched.
        public string? HandleKeywords(string input, ref string currentTopic)
        {
            input = input.ToLower();
            string topic = null;

            if (input.Contains("phishing"))
            {
                topic = "phishing";
            }
            else if (input.Contains("firewall") || input.Contains("firewalls"))
            {
                topic = "firewall";
            }
            else if (input.Contains("wifi") || input.Contains("public wifi"))
            {
                topic = "wifi";
            }
            else if (input.Contains("usb") || input.Contains("usbs"))
            {
                topic = "usb";
            }
            else if (input.Contains("social engineering"))
            {
                topic = "social engineering";
            }
            else if (input.Contains("password") || input.Contains("passwords"))
            {
                topic = "password";
            }
            else if (input.Contains("browse") || input.Contains("browsing"))
            {
                topic = "browse";
            }
            else
            {
                return null; // No match
            }

            var tip = tipManager.GetRandomTip(topic);
            var response = $"Cyber Buddy: {tip}\n\n*****************************************\n";

            LogAction($"User requested a tip on \"{topic}\"");

            return response;
        }

        public string? HandleNLPInput(string input, string userName)
        {
            var lowerInput = input.ToLower();

            // Reminders
            if (lowerInput.Contains("remind") || lowerInput.Contains("reminder"))
            {
                return HandleReminderInput(lowerInput);
            }
            // Tasks
            else if (lowerInput.Contains("add a task") || lowerInput.Contains("set a task") || lowerInput.Contains("add task"))
            {
                return HandleTaskInput(lowerInput);
            }
            else
            {
                string? sentimentResponse = sentimentManager.DetectAndRespond(lowerInput, userName, out string detectedEmotion, out string detectedTopic);
                if (sentimentResponse != null)
                {
                    LogAction($"User expressed emotion: {detectedEmotion} about topic: {detectedTopic}");
                    return sentimentResponse;
                }
                string? tipResponse = tipManager.HandleTipRequest(lowerInput, ref currentTopic);
                if (!string.IsNullOrEmpty(tipResponse))
                {
                    LogAction($"User asked for a tip about topic: {currentTopic}");
                    return tipResponse;
                }

                string? explanationResponse = explanationManager.HandleExplanation(lowerInput, ref currentTopic);
                if (!string.IsNullOrEmpty(explanationResponse))
                {
                    LogAction($"User asked for explanation of topic: {currentTopic}");
                    return explanationResponse;
                }

                string? interestResponse = memory.HandleInterestInput(lowerInput);
                if (interestResponse != null)
                {
                    // Log user interest topic
                    LogAction($"User expressed interest in topic: {memory.FavoriteTopic}");
                    return interestResponse;
                }
            }

            // Recent Actions
            if (lowerInput.Contains("what have you done") || lowerInput.Contains("recent"))
            {
                return ShowRecentActions();
            }
            // No match
            else
            {
                return null;
            }
        }

        private string? HandleReminderInput(string userInput)
        {
            var matchSetReminder = Regex.Match(userInput,
                @"set reminder for (?<title>.+) in (?<days>\d+) days?",
                RegexOptions.IgnoreCase);

            if (matchSetReminder.Success)
            {
                var taskTitle = matchSetReminder.Groups["title"].Value.Trim();
                var daysStr = matchSetReminder.Groups["days"].Value;

                if (int.TryParse(daysStr, out int days) && days > 0)
                {
                    var reminderDate = DateTime.Now.AddDays(days);
                    var task = taskManager.GetAllTasks()
                        .FirstOrDefault(t => t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));

                    if (task != null)
                    {
                        task.ReminderDate = reminderDate;

                        recentActions.Add($"Reminder set for task \"{task.Title}\" in {days} days (on {reminderDate:yyyy-MM-dd})");
                        LogAction($"Reminder set for task \"{task.Title}\" in {days} days (on {reminderDate:yyyy-MM-dd})");

                        return $"Cyber Buddy: Reminder set for task \"{task.Title}\" in {days} days, on {reminderDate:yyyy-MM-dd}.\n";
                    }
                    else
                    {
                        return $"Cyber Buddy: Task titled \"{taskTitle}\" not found.\n";
                    }
                }
                else
                {
                    return "Cyber Buddy: Please specify a valid number of days.";
                }
            }

            if (!string.IsNullOrEmpty(lastAddedTaskTitle))
            {
                var matchRemindMeInDays = Regex.Match(userInput,
                    @"remind me in (?<days>\d+) days?",
                    RegexOptions.IgnoreCase);

                if (matchRemindMeInDays.Success)
                {
                    var daysStr = matchRemindMeInDays.Groups["days"].Value;

                    if (int.TryParse(daysStr, out int days) && days > 0)
                    {
                        var reminderDate = DateTime.Now.AddDays(days);
                        var task = taskManager.GetAllTasks()
                            .FirstOrDefault(t => t.Title.Equals(lastAddedTaskTitle, StringComparison.OrdinalIgnoreCase));

                        if (task != null)
                        {
                            task.ReminderDate = reminderDate;

                            recentActions.Add($"Reminder set for task \"{task.Title}\" in {days} days (on {reminderDate:yyyy-MM-dd})");
                            LogAction($"Reminder set for task \"{task.Title}\" in {days} days (on {reminderDate:yyyy-MM-dd})");

                            return $"Cyber Buddy: Reminder set for task \"{task.Title}\" in {days} days, on {reminderDate:yyyy-MM-dd}.\n";
                        }
                        else
                        {
                            return $"Cyber Buddy: No task found with title \"{lastAddedTaskTitle}\".\n";
                        }
                    }
                    else
                    {
                        return "Cyber Buddy: Please specify a valid number of days.";
                    }
                }
            }
            else
            {
                return "Cyber Buddy: Please add a task first before setting a reminder.";
            }


            return "Cyber Buddy: Please specify a reminder like:\n- \"set reminder for TaskTitle in X days\"\n- \"remind me in X days\".";
        }

        private string? HandleTaskInput(string userInput)
        {
            var matchWithDesc = System.Text.RegularExpressions.Regex.Match(
                userInput,
                @"add (a )?task (to )?(?<title>.*?) with description (?<description>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );

            if (matchWithDesc.Success)
            {
                var taskTitle = matchWithDesc.Groups["title"].Value.Trim();
                var description = matchWithDesc.Groups["description"].Value.Trim();

                taskManager.AddTask(taskTitle, description);
                lastAddedTaskTitle = taskTitle;
                recentActions.Add($"Task added: \"{taskTitle}\"");
                LogAction($"Task added: \"{taskTitle}\"");
                return $"Cyber Buddy: Task \"{taskTitle}\" added with description \"{description}\".\n" +
                       "If you’d like to set a reminder, just say \"Remind me on '2025-01-01'\".";
            }
            var match = System.Text.RegularExpressions.Regex.Match(userInput, @"add (a )?task (to )?(.+)");
            if (!match.Success) match = System.Text.RegularExpressions.Regex.Match(userInput, @"set (a )?task (to )?(.+)");

            if (match.Success)
            {
                var taskTitle = match.Groups[2].Value.Trim();
                var description = "Added via NLP command";
                taskManager.AddTask(taskTitle, description);
                lastAddedTaskTitle = taskTitle;
                recentActions.Add($"Task added: \"{taskTitle}\"");
                LogAction($"Task added: \"{taskTitle}\"");
                return $"Cyber Buddy: Task \"{taskTitle}\" added with no description.\nIf you’d like to set a reminder, just say \"Remind me in 5 days'\".\n";
            }
            else
            {
                return "Cyber Buddy: What task would you like to add?\n";
            }
        }

        private string? ShowRecentActions()
        {
            if (recentActions.Count == 0)
            {
                return "Cyber Buddy: No recent activity.\n";
            }
            else
            {
                var summary = "Cyber Buddy: Here’s a summary of recent actions:\n";
                foreach (var action in recentActions)
                {
                    summary += $"- {action}\n";
                }
                return summary;
            }
        }


        private int displayedCount = 0;      // How many activities are currently displayed
        private const int pageSize = 10;     // Show 10 at a time

        public string ShowActivityLog(bool showMore = false)
        {
            if (activityLog.Count == 0)
                return "Cyber Buddy: No recent activity.";

            if (showMore)
            {
                displayedCount += pageSize;
            }
            else
            {
                displayedCount = pageSize;
            }
            if (displayedCount > activityLog.Count)
            {
                displayedCount = activityLog.Count;
            }

            var recent = activityLog.Skip(activityLog.Count - displayedCount).Take(displayedCount).ToList();

            var summary = $"\nCyber Buddy: Showing {displayedCount} of {activityLog.Count} activities:\n";

            for (int i = 0; i < recent.Count; i++)
            {
                summary += $"{i + 1}. {recent[i]}\n";
            }

            if (displayedCount < activityLog.Count)
            {
                summary += "\nType 'show more' to see additional activities.";
            }
            return summary;
        }
        public void LogAction(string actionDescription)
        {
           
            activityLog.Add(actionDescription);

            displayedCount = 0;
        }
    }
}
