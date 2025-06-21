using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Cybersecurity_Chatbot
{
    public class TipsManager
    {
        ASCII_Effects asciiArt = new ASCII_Effects();

        private readonly Dictionary<string, List<string>> tips = new Dictionary<string, List<string>>() //(Stack Overflow, 2023)
        {
            { "password", new List<string>
                {
                    "Use long complex passwords combining uppercase, lowercase letters, numbers, and special characters. Longer and more varied passwords are exponentially harder to crack. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Avoid using easily guessable info like names, birthdays or common words. Attackers often try these first during brute force or dictionary attacks. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Never reuse passwords across different sites. A breach on one service can compromise all your accounts if passwords are reused. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Use a reputable password manager to generate and store unique passwords safely, so you don't have to memorize them. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Enable two-factor authentication (2FA) wherever possible. This adds a second layer of security beyond just the password, such as a code on your phone. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Regularly update your passwords, especially after hearing about security breaches. Old passwords may have been exposed. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Be cautious about phishing attempts aimed at stealing your passwords; always verify the source before entering credentials. " +
                    "If you need another tip you can type more or explain passwords for an explanation."
                }
            },
            { "passwords", new List<string>
                {
                    "Use long, complex passwords combining uppercase, lowercase letters, numbers, and special characters. Longer and more varied passwords are exponentially harder to crack. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Avoid using easily guessable info like names, birthdays, or common words. Attackers often try these first during brute force or dictionary attacks. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Never reuse passwords across different sites. A breach on one service can compromise all your accounts if passwords are reused. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Use a reputable password manager to generate and store unique passwords safely, so you don't have to memorize them. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Enable two-factor authentication (2FA) wherever possible. This adds a second layer of security beyond just the password, such as a code on your phone. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Regularly update your passwords, especially after hearing about security breaches. Old passwords may have been exposed. " +
                    "If you need another tip you can type more or explain passwords for an explanation.",
                    "Be cautious about phishing attempts aimed at stealing your passwords; always verify the source before entering credentials. " +
                    "If you need another tip you can type more or explain passwords for an explanation."
                }
            },

            { "phishing", new List<string>
                {
                    "Verify the sender’s email address carefully; attackers often spoof legitimate addresses or use similar-looking domains. " +
                    "If you need another tip you can type more or explain phishing for an explanation.",
                    "Never provide personal info or passwords in response to unsolicited messages—legitimate organizations won’t ask this way. " +
                    "If you need another tip you can type more or explain phishing for an explanation.",
                    "Watch for spelling and grammar errors; many phishing emails contain mistakes that can be a red flag. " +
                    "If you need another tip you can type more or explain phishing for an explanation.",
                    "Hover over links before clicking to see if the URL looks suspicious or mismatched with the supposed sender. " +
                    "If you need another tip you can type more or explain phishing for an explanation.",
                    "Use email filters and spam detection to reduce the chances of phishing emails reaching your inbox. " +
                    "If you need another tip you can type more or explain phishing for an explanation.",
                    "Report phishing attempts to your email provider or IT department to help protect others. " +
                    "If you need another tip you can type more or explain phishing for an explanation.",
                    "Stay informed about current phishing techniques, as attackers constantly evolve their methods. " +
                    "If you need another tip you can type more or explain phishing for an explanation."
                }
            },
            
            { "firewall", new List<string>
                {
                    "Ensure your firewall is enabled at all times; it acts as a gatekeeper, filtering out potentially harmful traffic before it reaches your system. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Keep your firewall software updated so it can recognize and block the latest threats effectively. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Customize firewall rules to control which applications and services can access the internet, reducing the attack surface. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Use both hardware and software firewalls for layered defense, protecting both your network and individual devices. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Regularly review firewall logs to detect unusual patterns that might indicate attempted intrusions or malware. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Avoid disabling your firewall even temporarily, as this exposes your system to vulnerabilities during that window. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Learn how to safely whitelist trusted apps and block suspicious ones to maintain smooth operation without sacrificing security. " +
                    "If you need another tip you can type more or explain firewalls for an explanation."
                }
            },
            { "firewalls", new List<string>
                {
                    "Ensure your firewall is enabled at all times; it acts as a gatekeeper, filtering out potentially harmful traffic before it reaches your system. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Keep your firewall software updated so it can recognize and block the latest threats effectively. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Customize firewall rules to control which applications and services can access the internet, reducing the attack surface. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Use both hardware and software firewalls for layered defense, protecting both your network and individual devices. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Regularly review firewall logs to detect unusual patterns that might indicate attempted intrusions or malware. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Avoid disabling your firewall even temporarily, as this exposes your system to vulnerabilities during that window. " +
                    "If you need another tip you can type more or explain firewalls for an explanation.",
                    "Learn how to safely whitelist trusted apps and block suspicious ones to maintain smooth operation without sacrificing security. " +
                    "If you need another tip you can type more or explain firewalls for an explanation."
                }
            },

            { "browse", new List<string>
                {
                    "Only enter sensitive data on sites using HTTPS. This encrypts your data so hackers can’t easily intercept it during transmission. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Avoid clicking on pop-ups or ads from unknown sources—they can lead to malicious sites or downloads. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Keep your browser and all plugins up to date to patch security vulnerabilities hackers might exploit. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Use privacy-focused extensions like HTTPS Everywhere and uBlock Origin to improve security and block trackers. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Use private or incognito browsing modes when on shared or public computers to avoid leaving browsing history or cookies behind. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Clear your browser’s cookies and cache regularly to prevent tracking and remove outdated data. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Be cautious when downloading files from the web—only download from trusted sources to avoid malware. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation."
                }
            },
            { "browsing", new List<string>
                {
                    "Only enter sensitive data on sites using HTTPS. This encrypts your data so hackers can’t easily intercept it during transmission. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Avoid clicking on pop-ups or ads from unknown sources—they can lead to malicious sites or downloads. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Keep your browser and all plugins up to date to patch security vulnerabilities hackers might exploit. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Use privacy-focused extensions like HTTPS Everywhere and uBlock Origin to improve security and block trackers. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Use private or incognito browsing modes when on shared or public computers to avoid leaving browsing history or cookies behind. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Clear your browser’s cookies and cache regularly to prevent tracking and remove outdated data. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation.",
                    "Be cautious when downloading files from the web—only download from trusted sources to avoid malware. " +
                    "If you need another tip you can type more or explain safe browsing for an explanation."
                }
            },

            { "wifi", new List<string>
                {
                    "Avoid conducting sensitive transactions like banking on public Wi-Fi networks, which are often unsecured and vulnerable to eavesdropping. " +
                    "If you need another tip you can type more or explain wifi for an explanation.",
                    "Use a reliable VPN to encrypt your internet traffic when on public networks, preventing attackers from intercepting your data. " +
                    "If you need another tip you can type more or explain wifi for an explanation.",
                    "Always log out of websites and apps after use, especially on public Wi-Fi, to prevent session hijacking. " +
                    "If you need another tip you can type more or explain wifi for an explanation.",
                    "Disable automatic Wi-Fi connections to avoid connecting to fake or malicious hotspots without realizing it. " +
                    "If you need another tip you can type more or explain wifi for an explanation.",
                    "Turn off file sharing and AirDrop on your device when connected to public networks to reduce exposure. " +
                    "If you need another tip you can type more or explain wifi for an explanation.",
                    "Be wary of Wi-Fi network names that mimic legitimate networks but have slight differences, often used by attackers to trick users. " +
                    "If you need another tip you can type more or explain wifi for an explanation.",
                    "If possible, prefer mobile data over public Wi-Fi for sensitive activities. " +
                    "If you need another tip you can type more or explain wifi for an explanation."
                }
            },

            { "usb", new List<string>
                {
                    "Never plug in USB drives from unknown sources, as they can contain malware that infects your system automatically. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Always scan USB devices with updated antivirus software before opening any files to catch potential threats early. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Disable USB autorun/autoplay to prevent malware from executing as soon as the device is connected. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Clearly label your personal USB drives to avoid confusion and accidental use of unknown devices. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Avoid sharing USB drives in public or untrusted environments to reduce infection risk. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Use USB data blockers when charging devices in public places; these prevent data exchange while allowing power transfer. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Consider encrypting sensitive data stored on USB drives to protect it in case of loss or theft. " +
                    "If you need another tip you can type more or explain usbs for an explanation."
                }
            },
            { "usbs", new List<string>
                {
                    "Never plug in USB drives from unknown sources, as they can contain malware that infects your system automatically. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Always scan USB devices with updated antivirus software before opening any files to catch potential threats early. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Disable USB autorun/autoplay to prevent malware from executing as soon as the device is connected. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Clearly label your personal USB drives to avoid confusion and accidental use of unknown devices. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Avoid sharing USB drives in public or untrusted environments to reduce infection risk. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Use USB data blockers when charging devices in public places; these prevent data exchange while allowing power transfer. " +
                    "If you need another tip you can type more or explain usbs for an explanation.",
                    "Consider encrypting sensitive data stored on USB drives to protect it in case of loss or theft. " +
                    "If you need another tip you can type more or explain usbs for an explanation."
                }
            },

            { "social engineering", new List<string>
                {
                    "Always verify the identity of anyone requesting sensitive information—call back using known numbers, not those provided. " +
                    "If you need another tip you can type more or explain social engineering for an explanation.",
                    "Limit the amount of personal information shared on social media, as attackers use this info to build trust or guess security answers. " +
                    "If you need another tip you can type more or explain social engineering for an explanation.",
                    "Be alert to emotional manipulation, such as urgency or fear tactics, designed to pressure you into quick decisions. " +
                    "If you need another tip you can type more or explain social engineering for an explanation.",
                    "Never share passwords or security questions over phone or email, even if the request seems urgent or official. " +
                    "If you need another tip you can type more or explain social engineering for an explanation.",
                    "Educate your team or family on recognizing social engineering tactics to reduce risks across your network. " +
                    "If you need another tip you can type more or explain social engineering for an explanation.",
                    "Trust your instincts—if a request or message seems suspicious, pause and verify through independent channels. " +
                    "If you need another tip you can type more or explain social engineering for an explanation.",
                    "Use multifactor authentication to add a barrier against social engineering attacks that rely on stolen credentials. " +
                    "If you need another tip you can type more or explain social engineering for an explanation."
                }
            }
        };

        private Random random = new Random();//(Microsoft, 2025)
        public string GetRandomTip(string topic)
        {
            topic = topic.ToLower();

            if (tips.ContainsKey(topic))
            {

                var topicTips = tips[topic]; 
                return topicTips[random.Next(topicTips.Count)]; 
            }

            return "Sorry, I don't have any tips on that topic yet.";
        }

        public string? HandleTipRequest(string input, ref string currentTopic)
        {
            input = input.ToLower();

            // List of known topics
            string[] topics = { "password", "phishing", "firewall", "usb", "usbs ", "wifi", "social engineering", "browse" };
            bool containsTopic = false;

            // If the user mentions a new topic, set it
            foreach (string topic in topics)
            {
                if (input.Contains(topic))
                {
                    currentTopic = topic;
                    containsTopic = true;

                    return $"Cyber Buddy:\n{GetRandomTip(topic)}\n" +
                           $"\n*****************************************\n";
                }
            }

            // If the user says "more" or "another tip" and we already have a topic
            if ((input.Contains("more") || input.Contains("another tip") || input.Contains("tip") || input.Contains("advice")) && !string.IsNullOrEmpty(currentTopic))
            {
                return $"Cyber Buddy:\n{GetRandomTip(currentTopic)}\n" +
                       $"\n*****************************************\n";
            }

            // No known topic
            if ((input.Contains("more") || input.Contains("another tip") || input.Contains("tip") || input.Contains("advice")) && string.IsNullOrEmpty(currentTopic))
            {
                return "Cyber Buddy: What topic do you want a tip on? Type 'keywords' if you're not sure.\n" +
                       "\n*****************************************\n";
            }

            // Not a tip request
            return null;
        }
    }
}

/*Refrence List:
 Stack Overflow, 2023. Dictionary with list of strings as value. [online] Available at:
 https://stackoverflow.com/questions/17887407/dictionary-with-list-of-strings-as-value
 (Accessed: 21 May 2025).

 Microsoft, 2025. Random Class. [online] Available at: 
 https://learn.microsoft.com/en-us/dotnet/api/system.random?view=net-9.0 
 (Accessed: 21 May 2025)
 
 */