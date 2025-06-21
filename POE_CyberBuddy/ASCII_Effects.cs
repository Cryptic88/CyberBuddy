using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace POE_Cybersecurity_Chatbot
{
    public class ASCII_Effects
    {
        //ASCII art for the chatbot
        public string ShowAsciiArt() => @"
        
            

            _________        ___.                  __________          .___  .___      
            \_   ___ \___.__.\_ |__   ___________  \______   \__ __  __| _/__| _/__.__.
            /    \  \<   |  | | __ \_/ __ \_  __ \  |    |  _/  |  \/ __ |/ __ <   |  |
            \     \___\___  | | \_\ \  ___/|  | \/  |    |   \  |  / /_/ / /_/ |\___  |
             \______  / ____| |___  /\___  >__|     |______  /____/\____ \____ |/ ____|
                    \/\/          \/     \/                \/           \/    \/\/     
        "; //(patorjk, 2025)
        

        public string LoadingEffect(int cycles = 1) =>
            "Loading Cyber Buddy" + new string('.', cycles); //(stackoverflow, 2016)


        public string HeaderEffect() => @"
            ==============================
              Cyber Buddy Chat Activated
            ==============================
        ";

        private readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public void Speak(string text)
        {
            synthesizer.Rate = -1;
            synthesizer.Volume = 100;
            synthesizer.SelectVoice("Microsoft Zira Desktop");
            synthesizer.SpeakAsync(RemoveAscii(text));
        }


        // Helper method to strip ASCII art or non-speechable text
        private string RemoveAscii(string input) =>
            input.Replace("Cyber Buddy:", "").Trim();

public string ShowAsciiArtForTopic(string topic)
{
    switch (topic.ToLower())
    {
        case var t when t.Contains("wifi"):
            return @"

            ______      _     _ _        _    _ _  __ _ 
            | ___ \    | |   | (_)      | |  | (_)/ _(_)
            | |_/ /   _| |__ | |_  ___  | |  | |_| |_ _ 
            |  __/ | | | '_ \| | |/ __| | |/\| | |  _| |
            | |  | |_| | |_) | | | (__  \  /\  / | | | |
            \_|   \__,_|_.__/|_|_|\___|  \/  \/|_|_| |_|    
            
            ";

        case var t when t.Contains("firewall"):
            return @"

             (    (   (                       (    (     
             )\ ) )\ ))\ )    (  (      (     )\ ) )\ )  
            (()/((()/(()/((   )\))(   ' )\   (()/((()/(  
             /(_))/(_))(_))\ ((_)()\ |(((_)(  /(_))/(_)) 
            (_))_(_))(_))((_)_(())\_)()\ _ )\(_)) (_))   
            | |_ |_ _| _ \ __\ \((_)/ (_)_\(_) |  | |    
            | __| | ||   / _| \ \/\/ / / _ \ | |__| |__  
            |_|  |___|_|_\___| \_/\_/ /_/ \_\|____|____|    
            
            ";

        case var t when t.Contains("usb"):
            return @"

            _     _ _______ ______       ______   ______ _____ _    _ _______ _______
            |     | |______ |_____]      |     \ |_____/   |    \  /  |______ |______
            |_____| ______| |_____]      |_____/ |    \_ __|__   \/   |______ ______|
            
            ";

        case var t when t.Contains("social engineering"):
            return @"

               _____            _       __   ______            _                     
              / ___/____  _____(_)___ _/ /  / ____/___  ____ _(_)___  ___  ___  _____
              \__ \/ __ \/ ___/ / __ `/ /  / __/ / __ \/ __ `/ / __ \/ _ \/ _ \/ ___/
             ___/ / /_/ / /__/ / /_/ / /  / /___/ / / / /_/ / / / / /  __/  __/ /    
            /____/\____/\___/_/\__,_/_/  /_____/_/ /_/\__, /_/_/ /_/\___/\___/_/     
                                                     /____/                         
            ";

        case var t when t.Contains("phishing"):
            return @"

               ___ _     _     _     _             
              / _ \ |__ (_)___| |__ (_)_ __   __ _ 
             / /_)/ '_ \| / __| '_ \| | '_ \ / _` |
            / ___/| | | | \__ \ | | | | | | | (_| |
            \/    |_| |_|_|___/_| |_|_|_| |_|\__, |
                                             |___/
            ";

        case var t when t.Contains("password"):
            return @"

              _____                                    _     
             |  __ \                                  | |    
             | |__) |_ _ ___ _____      _____  _ __ __| |___ 
             |  ___/ _` / __/ __\ \ /\ / / _ \| '__/ _` / __|
             | |  | (_| \__ \__ \\ V  V / (_) | | | (_| \__ \
             |_|   \__,_|___/___/ \_/\_/ \___/|_|  \__,_|___/
            
            ";

        case var t when t.Contains("browse"):
            return @"

                ____                                  _____       ____     __     
               / __ )_________ _      __________     / ___/____ _/ __/__  / /_  __
              / __  / ___/ __ \ | /| / / ___/ _ \    \__ \/ __ `/ /_/ _ \/ / / / /
             / /_/ / /  / /_/ / |/ |/ (__  )  __/   ___/ / /_/ / __/  __/ / /_/ / 
            /_____/_/   \____/|__/|__/____/\___/   /____/\__,_/_/  \___/_/\__, /  
                                                                         /____/  
            ";

        default:
            return ""; // No special art
    }
}

    }
}
/*Refrence List:
  
  patorjk, 2025. Text to ASCII Art Generator (TAAG). [online] Available at:
  https://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20
  [Accessed 10 April 2025].

  stackoverflow, 2016. c# how to make loading text simple. [online] Available at:
  https://stackoverflow.com/questions/39694698/c-sharp-how-to-make-loading-text-simple
  [Accessed 11 April 2025].
 
  stackoverflow, 2012. Wrap text to the next line when it exceeds a certain length?. [online] Available at:
  https://stackoverflow.com/questions/10541124/wrap-text-to-the-next-line-when-it-exceeds-a-certain-length
  [Accessed 11 April 2025].*/
