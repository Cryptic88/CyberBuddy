using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Cybersecurity_Chatbot
{
    public class Memory
    {

        public string Name { get; set; }
        public string FavoriteTopic { get; set; }

        public string HandleInterestInput(string input)
        {
            input = input.ToLower();

            if (input.Contains("interested in") || input.Contains("like to learn"))
            {
                string[] knownTopics = { "phishing", "passwords", "password", "privacy", "firewalls", "firewall", "usb", "usbs", "wifi", "social engineering", "browse", "browsing" };

                foreach (var topic in knownTopics)
                {
                    if (input.Contains(topic))
                    {
                        FavoriteTopic = topic;

                        return $"Cyber Buddy: Great! I'll remember that you're interested in {topic}." +
                            $" It's an important part of staying safe online.\n" +
                            $"\n*****************************************\n";

                    }
                }
                return "Cyber Buddy: I’d love to remember your interests. Could you be more specific, like 'I'm interested in phishing'?\n" +
                    "\n*****************************************\n";
            
        }

            return null;
        }
    }
}
