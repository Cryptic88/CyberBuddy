namespace POE_Cybersecurity_Chatbot
{
    public class ConversationPart2
    {
        private ASCII_Effects asciiArt;
        private TipsManager tipManager;

        public ConversationPart2(ASCII_Effects asciiArt, TipsManager tipManager)
        {
            this.asciiArt = asciiArt;
            this.tipManager = tipManager;
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
    }
}
