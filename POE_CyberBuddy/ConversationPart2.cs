using POE_CyberBuddy;

namespace POE_Cybersecurity_Chatbot
{
    public class ConversationPart2
    {
        
        private TipsManager tipManager;
        private TaskManager taskManager;
        private string? lastAddedTaskTitle = null;

        private List<string> recentActions = new List<string>();

        public ConversationPart2(ASCII_Effects asciiArt, TipsManager tipManager, TaskManager taskManager)
        {
            
            this.tipManager = tipManager;
            this.taskManager = taskManager;
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

            return response;
        }

        public string? HandleNLPInput(string input)
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
            // Recent Actions
            else if (lowerInput.Contains("what have you done") || lowerInput.Contains("recent"))
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
            if (string.IsNullOrEmpty(lastAddedTaskTitle))
            {
                return "Cyber Buddy: Please add a task first before setting a reminder.";
            }

            // Check for "remind me on yyyy-MM-dd"
            var matchOnDate = System.Text.RegularExpressions.Regex.Match(
                userInput,
                @"remind me on (?<date>\d{4}-\d{2}-\d{2})",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );

            if (matchOnDate.Success)
            {
                var dateStr = matchOnDate.Groups["date"].Value;
                if (DateTime.TryParse(dateStr, out var reminderDate))
                {
                    var task = taskManager.GetAllTasks()
                        .FirstOrDefault(t => t.Title.Equals(lastAddedTaskTitle, StringComparison.OrdinalIgnoreCase));

                    if (task != null)
                    {
                        task.ReminderDate = reminderDate;
                        recentActions.Add($"Reminder set for task \"{task.Title}\" on {reminderDate:yyyy-MM-dd}");
                        return $"Cyber Buddy: Reminder set for task \"{task.Title}\" on {reminderDate:yyyy-MM-dd}.\n";
                    }
                }
                return $"Cyber Buddy: The date \"{dateStr}\" is not valid or no task found.";
            }

            return "Cyber Buddy: Please specify a date like 'Remind me on yyyy-MM-dd'.\n";
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
                return $"Cyber Buddy: Task \"{taskTitle}\" added with no description.\nIf you’d like to set a reminder, just say \"Remind me to... on '2025-01-01'\".";
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
    }
}
