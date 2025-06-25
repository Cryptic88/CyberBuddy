using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POE_CyberBuddy;

namespace POE_Cybersecurity_Chatbot
{
    public class ExplanationManager
    {

        private Dictionary<string, List<string>> explanations = new Dictionary<string, List<string>>() //(Stack Overflow, 2023)
        {
            {
                "password", new List<string>()
                {
                    "Passwords are secret codes or words used to protect your personal information and accounts. "+
                    "A strong password helps prevent unauthorized access to your online services. It should be complex and unique. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "Passwords serve as your digital keys, granting access to sensitive accounts and personal data. A strong password prevents unauthorized users from breaking in by making guessing or brute-force attacks impractical. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "The complexity of a password — including uppercase and lowercase letters, numbers, and symbols — drastically increases the number of possible combinations, making it exponentially harder for attackers to crack. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "Reusing the same password across multiple sites is risky because if one site is breached, attackers can try that password elsewhere, a technique known as credential stuffing. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "Regularly changing passwords limits the time window attackers have to exploit stolen credentials. Using passphrases (multiple words strung together) can be easier to remember and just as strong. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic."
                }
            },
            {
                "passwords", new List<string>()
                {
                    "Passwords are secret codes used to protect your personal information and accounts. "+
                    "A strong password helps prevent unauthorized access to your online services. It should be complex and unique. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "Passwords serve as your digital keys, granting access to sensitive accounts and personal data. A strong password prevents unauthorized users from breaking in by making guessing or brute-force attacks impractical. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "The complexity of a password — including uppercase and lowercase letters, numbers, and symbols — drastically increases the number of possible combinations, making it exponentially harder for attackers to crack. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "Reusing the same password across multiple sites is risky, because if one site is breached, attackers can try that password elsewhere, a technique known as credential stuffing. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic.",
                    "Regularly changing passwords limits the time window attackers have to exploit stolen credentials. Using passphrases (multiple words strung together) can be easier to remember and just as strong. " +
                    "If you need another explanation you can type explain passwords or type more for a tip about this topic."
                }
            },
            {
                "phishing", new List<string>()
                {
                    "Phishing is a type of cyberattack where attackers impersonate trusted entities to steal personal information. "+
                    "These scams often come in the form of fake emails or messages that trick you into clicking malicious links. " +
                    "If you need another explanation you can type explain phishing or type more for a tip about this topic.",
                    "Phishing attacks exploit human psychology, tricking victims into revealing passwords, credit card numbers, or other sensitive information by pretending to be a trusted source. " +
                    "If you need another explanation you can type explain phishing or type more for a tip about this topic.",
                    "Attackers often create emails or messages that look identical to legitimate companies, using logos, formats, and language that lower suspicion and encourage quick action. " +
                    "If you need another explanation you can type explain phishing or type more for a tip about this topic.",
                    "These scams frequently use urgent language such as 'Your account will be suspended!' to pressure victims into clicking malicious links or opening dangerous attachments without thinking. " +
                    "If you need another explanation you can type explain phishing or type more for a tip about this topic.",
                    "Successful phishing can lead to identity theft, financial fraud, or malware infections, making user awareness and cautious behavior crucial defenses. " +
                    "If you need another explanation you can type explain phishing or type more for a tip about this topic."
                }
            },
            {
                "firewall", new List<string>()
                {
                    "A firewall is a security system that monitors and filters network traffic to block harmful connections. "+
                    "It acts as a barrier between your device and the internet, helping prevent unauthorized access. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "Firewalls act as security gatekeepers, filtering network traffic based on predefined security rules to block unauthorized access while allowing legitimate communication. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "There are two main types: hardware firewalls, which protect entire networks, and software firewalls, which protect individual devices, often working together for better security. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "Firewalls can block incoming threats like hackers or worms and prevent unauthorized outbound traffic, stopping compromised devices from communicating with attackers. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "Regular updates and careful configuration are essential, as poorly set rules or outdated software can leave vulnerabilities exploitable by attackers. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic."
                }
            },
            {
                "firewalls", new List<string>()
                {
                    "A firewall is a security system that monitors and filters network traffic to block harmful connections. "+
                    "It acts as a barrier between your device and the internet, helping prevent unauthorized access. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "Firewalls act as security gatekeepers, filtering network traffic based on predefined security rules to block unauthorized access while allowing legitimate communication. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "There are two main types: hardware firewalls, which protect entire networks, and software firewalls, which protect individual devices, often working together for better security. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "Firewalls can block incoming threats like hackers or worms and prevent unauthorized outbound traffic, stopping compromised devices from communicating with attackers. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic.",
                    "Regular updates and careful configuration are essential, as poorly set rules or outdated software can leave vulnerabilities exploitable by attackers. " +
                    "If you need another explanation you can type explain firewalls or type more for a tip about this topic."
                }
            },
            {
                "usb", new List<string>()
                {
                    "USB drives can carry malware if they come from unknown or untrusted sources. "+
                    "Avoid plugging in unrecognized USB devices as they might compromise your system’s security. " +
                    "If you need another explanation you can type explain usbs or type more for a tip about this topic.",
                    "USB drives, while convenient for transferring data, can unknowingly spread malware, including viruses and ransomware, especially when their origin is unknown or untrusted. " +
                    "If you need another explanation you can type explain usbs or type more for a tip about this topic.",
                    "Malicious USBs may exploit the autorun feature on some systems, executing harmful code immediately upon connection, which can compromise the entire system. " +
                    "If you need another explanation you can type explain usbs or type more for a tip about this topic.",
                    "Disabling autorun is a critical step in preventing automatic infection from infected devices, requiring manual user action before files can be accessed. " +
                    "If you need another explanation you can type explain usbs or type more for a tip about this topic.",
                    "Proper labeling and controlling physical access to your own USB drives help avoid mix-ups and accidental use of potentially dangerous external drives. " +
                    "If you need another explanation you can type explain usbs or type more for a tip about this topic."
                }
            },
            {
                "wifi", new List<string>()
                {
                    "Public Wi-Fi networks are often unsecured, making it easy for attackers to intercept your data. "+
                    "Avoid doing sensitive activities like online banking on public Wi-Fi unless you use a VPN. " +
                    "If you need another explanation you can type explain wifi or type more for a tip about this topic.",
                    "Public Wi-Fi networks typically lack encryption, making it easy for attackers nearby to intercept and capture the data you send and receive, including passwords and personal information. " +
                    "If you need another explanation you can type explain wifi or type more for a tip about this topic.",
                    "Using a Virtual Private Network (VPN) creates an encrypted tunnel for your internet traffic, shielding it from prying eyes even on unsecured networks. " +
                    "If you need another explanation you can type explain wifi or type more for a tip about this topic.",
                    "After using public Wi-Fi, logging out of websites is critical to prevent session hijacking, where attackers use stolen session cookies to impersonate you. " +
                    "If you need another explanation you can type explain wifi or type more for a tip about this topic.",
                    "Disabling automatic connection to Wi-Fi networks avoids connecting unknowingly to fake hotspots set up by attackers to steal your data. " +
                    "If you need another explanation you can type explain wifi or type more for a tip about this topic."
                }
            },
            {
                "social engineering", new List<string>()
                {
                    "Social engineering is a tactic where attackers manipulate people into giving up confidential information. "+
                    "It relies more on human error than technical flaws—examples include fake tech support calls or urgent messages. " +
                    "If you need another explanation you can type explain social engineering or type more for a tip about this topic.",
                    "Social engineering attacks manipulate human emotions like fear, urgency, or helpfulness to trick people into bypassing normal security procedures. " +
                    "If you need another explanation you can type explain social engineering or type more for a tip about this topic.",
                    "Attackers might impersonate coworkers, IT staff, or government officials to gain trust and convince victims to reveal confidential information or perform unsafe actions. " +
                    "If you need another explanation you can type explain social engineering or type more for a tip about this topic.",
                    "Vigilance is key — verifying identities through separate communication channels and questioning unexpected requests can thwart these attacks. " +
                    "If you need another explanation you can type explain social engineering or type more for a tip about this topic.",
                    "Since social engineering exploits human nature rather than technical flaws, ongoing security awareness and training are vital in defending against it. " +
                    "If you need another explanation you can type explain social engineering or type more for a tip about this topic."
                }
            },
            {
                "browse", new List<string>()
                {
                    "Safe browsing involves using secure websites and being cautious with what you click online. "+
                    "Use updated browsers and avoid downloading files or extensions from untrusted sources. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Safe browsing starts with using HTTPS websites, which encrypt data exchanged with the server, protecting sensitive info like passwords and credit card details. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Many cyber attacks begin with malicious ads or pop-ups that either trick you into downloading malware or lead to phishing sites; using ad blockers can reduce this risk. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Regularly updating browsers and plugins patches security holes that hackers exploit to compromise systems or steal data. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Using privacy extensions can block trackers, prevent fingerprinting, and reduce your digital footprint, improving both security and privacy. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic."
                }
            },
            {
                "browsing", new List<string>()
                {
                    "Safe browsing involves using secure websites and being cautious with what you click online. "+
                    "Use updated browsers and avoid downloading files or extensions from untrusted sources. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Safe browsing starts with using HTTPS websites, which encrypt data exchanged with the server, protecting sensitive info like passwords and credit card details. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Many cyber attacks begin with malicious ads or pop-ups that either trick you into downloading malware or lead to phishing sites; using ad blockers can reduce this risk. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Regularly updating browsers and plugins patches security holes that hackers exploit to compromise systems or steal data. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic.",
                    "Using privacy extensions can block trackers, prevent fingerprinting, and reduce your digital footprint, improving both security and privacy. " +
                    "If you need another explanation you can type explain safe browsing or type more for a tip about this topic."
                }
            }
        };

        private Random rand = new Random(); //(Microsoft, 2025)

        public string GetExplanation(string topic)
        {
            if (explanations.ContainsKey(topic.ToLower()))
            {
                var topicList = explanations[topic.ToLower()];
                return topicList[rand.Next(topicList.Count)]; //(Microsoft, 2025)
            }
            else
            {
                return "Sorry, I don’t have an explanation for that topic yet.";
            }
        }

        public string? HandleExplanation(string input, ref string currentTopic)
        {
            input = input.ToLower();

            if (input.Contains("explain") || input.Contains("what is") || input.Contains("what do you mean"))
            {
                // List of known topics to look for in the input
                string[] topics = { "password","passwords", "phishing", "firewall", "firewalls", "usb", "usbs", "wifi", "social engineering", "browse","browsing" };

                foreach (string topic in topics)
                {
                    if (input.Contains(topic))
                    {
                        currentTopic = topic;
                        string explanation = GetExplanation(topic);
                        string response = $"Cyber Buddy: {explanation}\n\n*****************************************\n";
                        
                        return response;
                    }
                }

                return "Cyber Buddy: Please clarify what you'd like me to explain (e.g., passwords, phishing, firewalls).\n" +
                    "\n*****************************************\n";
            }

            return null; // Not an explanation-type input
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
