using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POE_CyberBuddy;

namespace POE_Cybersecurity_Chatbot
{
    public class Conversation
    {
        ASCII_Effects asciiArt = new ASCII_Effects();
        private ExplanationManager explanationManager;
        Memory memory = new Memory();
        private TipsManager tipManager = new TipsManager();
        private SentimentManager sentimentManager;
        private ConversationPart2 keywordHandler;
        private TaskManager taskManager = new TaskManager();
        private string currentTopic;

        public void Initialize(string userName)
        {
            memory.Name = userName;
            keywordHandler = new ConversationPart2(asciiArt, tipManager, taskManager);
            explanationManager = new ExplanationManager();
            sentimentManager = new SentimentManager();
        } //Variable to track the current topic of conversation.
        public string ShowWelcomeMessage(string userName) =>
            $"Welcome {userName}! You can select a wide range of questions or form your own questions with keywords.\n" +
            "You can also view all commands and keywords by clicking the 'Commands' button in the top right corner.\n";
        public string GetCommandList() =>
            "Here are all the commands and keywords you can try:\n" +
            "- like to learn [add favorite topic]\n" +
            "- Show activity log\n"+
            "- Add task 'task title' with description 'task description'\n"+
            "- how are you\n" +
            "- who are you\n" +
            "- help\n" +
            "- phishing\n" +
            "- firewall / firewalls\n" +
            "- wifi\n" +
            "- usb\n" +
            "- social engineering\n" +
            "- password / passwords\n" +
            "- browse / browsing\n" +
            "- Thank you / thanks\n" +
            "- clear\n" +
            "- exit\n";

        public string StartConversation(string userInput, string userName)
        {

            if (!string.IsNullOrEmpty(memory.FavoriteTopic) && new Random().Next(0, 5) == 0)
            {
                return $"Cyber Buddy: Just a reminder — since you're interested in {memory.FavoriteTopic}, you can ask about it any time using keywords like 'tips' or 'advice'.";
            }


            var nlpResponse = keywordHandler.HandleNLPInput(userInput, userName);
            if (!string.IsNullOrEmpty(nlpResponse))
            {
                return nlpResponse;
            }


            switch (userInput.ToLower()) // (W3schools, 2025)
            {
                case "how are you":
                    return $"Cyber Buddy: I'm just a bunch of code resembling a Cyber Security Chatbot, but I'm functioning well! Thanks for asking, {userName}.";

                case "who are you":
                    return "Cyber Buddy: I'm your Cyber Security Buddy — here to help you stay safe online.";

                case "thank you":
                case "thanks":
                    return $"Cyber Buddy: You're very welcome, {userName}! Let me know if you need help with anything else.";

                case "clear":
                    return "Cyber Buddy: Console cleared. Let's continue!";

                case "help":
                    return "Cyber Buddy: You can ask me about cybersecurity topics like phishing, passwords, browsing safely, and more. Type 'list' for available commands.";

                case "exit":
                    return $"Cyber Buddy: Goodbye {userName}, stay safe online!";

                default:

                    var foundKeywordResponse = keywordHandler.HandleKeywords(userInput, ref currentTopic);
                    if (!string.IsNullOrEmpty(foundKeywordResponse))
                    {
                        return foundKeywordResponse;
                    }

                    // Fallback if no match
                    var reply = $"Cyber Buddy: I didn't quite understand that, {userName}. Try again or type 'list' for available commands.";
                    if (!string.IsNullOrEmpty(memory.FavoriteTopic))
                    {
                        reply += $"\nSince you're interested in {memory.FavoriteTopic}, you can ask about it any time.";
                    }

                    return reply;
            }
        }

        

    }
}

/*Refrence List:
  Microsoft Learn, 2025. String interpolation using $. [online] Available at:
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
  [Accessed 10 April 2025].
  
  W3schools, 2025. C# Switch. [online] Available at:
  https://www.w3schools.com/cs/cs_switch.php
  [Accessed 10 April 2025].

  GeeksforGeeks, 2021. ref in C#. [online] Available at: 
  https://www.geeksforgeeks.org/ref-in-c-sharp/ 
  (Accessed: 21 May 2025).

  Microsoft, 2025b. Random.Next Method (System). [online] Available at: 
  https://learn.microsoft.com/en-us/dotnet/api/system.random.next?view=net-9.0 
  (Accessed: 22 May 2025).
 */
