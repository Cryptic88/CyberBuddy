using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Cybersecurity_Chatbot
{
    public class SentimentManager
    {
        private TipsManager tipsManager = new TipsManager();

        private Dictionary<string, string> sentimentResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) //(Stack Overflow, 2012)
        {
            { "worried", "It's completely understandable to feel worried. Let me share some tips to help you stay safe and delete those worries you have." },
            { "scared", "Feeling scared is normal when dealing with online threats. You're not alone — let's tackle this together!" },
            { "frustrated", "I hear you, cybersecurity can feel frustrating at times. I'm here to help simplify things for you." },
            { "confused", "No worries, it's okay to feel confused. Let's take it one step at a time." },
            { "anxious", "It’s okay to feel anxious. Cybersecurity is a big topic, but I’m here to guide you through it." },
            { "curious", "That's great! Curiosity is the first step to becoming cyber-smart. Ask me anything you're curious about." }
        };

        private List<string> topics = new List<string> { "password", "passwords", "phishing", "wifi", "usb", "usbs", "browse", "browsing", "social engineering", "firewalls", "firewall" };


        public string DetectAndRespond(string input, string userName)
        {
            foreach (var emotion in sentimentResponses.Keys)
            {
                if (input.IndexOf(emotion, StringComparison.OrdinalIgnoreCase) >= 0) //(Microsoft Learn, 2025)
                {
                    string emotionResponse = sentimentResponses[emotion];

                    // Check for a topic keyword in the same sentence
                    foreach (var topic in topics)
                    {
                        if (input.IndexOf(topic, StringComparison.OrdinalIgnoreCase) >= 0) //(Microsoft Learn, 2025)
                        {
                            // Combine emotional support + practical tip
                            string tip = tipsManager.GetRandomTip(topic);
                            return $"Cyber Buddy: {emotionResponse} Since you mentioned {topic}, here’s a quick tip to help:\n" +
                                $"Tip: {tip}\n" +
                                $"\n*****************************************\n";
                        }
                    }

                    // If no topic is mentioned, just give the emotional support
                    return $"Cyber Buddy: {emotionResponse}\n" +
                        $"\n*****************************************\n";
                }
            }
        

            return null; // No emotion detected
        }
    }

}
/*Refrence List:
    Stack Overflow, 2012. Case insensitive access for generic dictionary. [online] Available at:
    https://stackoverflow.com/questions/13230414/case-insensitive-access-for-generic-dictionary 
    (Accessed: 22 May 2025).
 
    Microsoft Learn, 2025. StringComparer.OrdinalIgnoreCase Property (Microsoft Docs). [online] Available at: 
    https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinalignorecase?view=net-9.0 
    (Accessed: 22 May 2025).
 
 */
