using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POE_Cybersecurity_Chatbot;
using static POE_CyberBuddy.QuizManager;

namespace POE_CyberBuddy
{
    public class InputProcessor
    {
        private readonly ConversationPart2 conversationPart2;
        private readonly TaskManager taskManager;
        private readonly SentimentManager sentimentManager;
        private readonly ExplanationManager explanationManager;
        private readonly QuizHandler quizHandler;
        private readonly Conversation chatbot;
        private readonly ASCII_Effects asciiArt;
        private readonly string userName;

        // UI callbacks
        private readonly Action<string> AppendTextToOutput;
        private readonly Action AppendScrollToBottom;
        private readonly Action ClearInput;
        private readonly Func<bool> IsInQuiz;
        private readonly Action<string> HandleQuizResponse;
        private readonly Func<string, string, string?> HandleNLPInput;


        private string currentTopic = "";

        public InputProcessor(
            ConversationPart2 conversationPart2,
            TaskManager taskManager,
            SentimentManager sentimentManager,
            QuizHandler quizHandler,
            ExplanationManager explanationManager,
            Conversation chatbot,
            ASCII_Effects asciiArt,
            string userName,
            Action<string> appendTextToOutput,
            Action appendScrollToBottom,
            Action clearInput,
            Func<bool> isInQuiz,
            Action<string> handleQuizResponse,
            Func<string, string, string?> handleNLPInput)
        {
            this.conversationPart2 = conversationPart2;
            this.taskManager = taskManager;
            this.sentimentManager = sentimentManager;
            this.explanationManager = explanationManager;
            this.chatbot = chatbot;
            this.asciiArt = asciiArt;
            this.userName = userName;
            this.quizHandler = quizHandler;

            AppendTextToOutput = appendTextToOutput;
            AppendScrollToBottom = appendScrollToBottom;
            ClearInput = clearInput;
            IsInQuiz = isInQuiz;
            HandleQuizResponse = handleQuizResponse;
            HandleNLPInput = handleNLPInput;
        }

        public async Task ProcessInput(string input)
        {
            input = input.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            if (quizHandler.IsInQuiz)
            {
                await quizHandler.HandleResponse(input);
                return;
            }

            if (input.Equals("show more", StringComparison.OrdinalIgnoreCase))
            {
                var reply = conversationPart2.ShowActivityLog(showMore: true);
                AppendTextToOutput($"You: {input}\n{reply}\n");
                asciiArt.Speak("Here are more activities from your log.");
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            if (input.Equals("show activity log", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("what have you done", StringComparison.OrdinalIgnoreCase))
            {
                var reply = conversationPart2.ShowActivityLog();
                AppendTextToOutput($"You: {input}\n{reply}\n");
                asciiArt.Speak("Here’s a summary of recent activity.");
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            if (input.Equals("list tasks", StringComparison.OrdinalIgnoreCase))
            {
                conversationPart2.LogAction("User requested to list all tasks.");

                var allTasks = taskManager.GetAllTasks();
                if (allTasks.Count == 0)
                {
                    string reply = "Cyber Buddy: You have no tasks.";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else
                {
                    string replyHeader = "Cyber Buddy: Here are your tasks:";
                    AppendTextToOutput(replyHeader + Environment.NewLine);
                    asciiArt.Speak(replyHeader);
                    foreach (var t in allTasks)
                    {
                        AppendTextToOutput(t.ToString() + Environment.NewLine);
                        asciiArt.Speak(t.ToString());
                    }
                }
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            if (input.StartsWith("complete task", StringComparison.OrdinalIgnoreCase))
            {
                var title = input.Substring("complete task".Length).Trim();
                if (string.IsNullOrEmpty(title))
                {
                    string reply = "Cyber Buddy: Please provide the title of the task you want to complete.";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else if (taskManager.CompleteTask(title))
                {
                    conversationPart2.LogAction($"User completed task: \"{title}\"");
                    string reply = $"Cyber Buddy: Marked task \"{title}\" as completed.";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else
                {
                    conversationPart2.LogAction($"User tried to complete a non-existing task: \"{title}\"");
                    string reply = $"Cyber Buddy: Couldn't find a task named \"{title}\".";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            if (input.StartsWith("delete task", StringComparison.OrdinalIgnoreCase))
            {
                var title = input.Substring("delete task".Length).Trim();
                if (string.IsNullOrEmpty(title))
                {
                    string reply = "Cyber Buddy: Please provide the title of the task you want to delete.";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else if (taskManager.DeleteTask(title))
                {
                    conversationPart2.LogAction($"User deleted task: \"{title}\"");
                    string reply = $"Cyber Buddy: Deleted Task: \"{title}\".";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                else
                {
                    conversationPart2.LogAction($"User tried to delete a non-existing task: \"{title}\"");
                    string reply = $"Cyber Buddy: Couldn't find a task named \"{title}\".";
                    AppendTextToOutput(reply + Environment.NewLine);
                    asciiArt.Speak(reply);
                }
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            string? detectedEmotion, detectedTopic;
            var sentimentResponse = sentimentManager.DetectAndRespond(input, userName, out detectedEmotion, out detectedTopic);

            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                conversationPart2.LogAction($"User expressed emotion: '{detectedEmotion}' with topic: '{detectedTopic}'");

                if (!string.IsNullOrEmpty(detectedTopic))
                {
                    AppendAsciiArt(asciiArt.ShowAsciiArtForTopic(detectedTopic));
                }

                AppendTextToOutput($"You: {input}\n");
                asciiArt.Speak(sentimentResponse);
                AppendTextToOutput(sentimentResponse + Environment.NewLine);
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            string? explanationResponse = explanationManager.HandleExplanation(input, ref currentTopic);
            if (!string.IsNullOrEmpty(explanationResponse))
            {
                conversationPart2.LogAction($"User requested an explanation for input: \"{input}\"");
                AppendAsciiArt(asciiArt.ShowAsciiArtForTopic(input));
                AppendTextToOutput($"You: {input}\n");
                asciiArt.Speak(explanationResponse);
                AppendTextToOutput($"Cyber Buddy: {explanationResponse}\n");
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            var nlpResponse = HandleNLPInput(input, userName);
            if (!string.IsNullOrEmpty(nlpResponse))
            {
                AppendAsciiArt(asciiArt.ShowAsciiArtForTopic(input));
                AppendTextToOutput($"You: {input}\n");
                asciiArt.Speak(nlpResponse);
                AppendTextToOutput(nlpResponse + Environment.NewLine);
                AppendScrollToBottom();
                ClearInput();
                return;
            }

            string botResponse = chatbot.StartConversation(input, userName);

            if (!string.IsNullOrEmpty(botResponse))
            {
                if (input.Contains("phishing", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("firewall", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("wifi", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("usb", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("social engineering", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("password", StringComparison.OrdinalIgnoreCase) ||
                    input.Contains("browse", StringComparison.OrdinalIgnoreCase))
                {
                    AppendAsciiArt(asciiArt.ShowAsciiArtForTopic(input));
                }

                AppendTextToOutput($"You: {input}\n\n");
                asciiArt.Speak(botResponse);
                AppendTextToOutput(botResponse + Environment.NewLine);
            }

            ClearInput();
            AppendScrollToBottom();
        }

        private void AppendAsciiArt(string ascii)
        {
            if (!string.IsNullOrEmpty(ascii))
            {
                AppendTextToOutput(ascii + Environment.NewLine);
                AppendScrollToBottom();
            }
        }
    }
}
