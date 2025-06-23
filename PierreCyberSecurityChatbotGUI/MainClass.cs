using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace PierreCyberSecurityBotPROG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Playing welcome audio...");


            string audioPath = @"C:\Users\lab_services_student\Downloads\PROG mp3 to convert.wav";

            SoundPlayer player = new SoundPlayer(audioPath);
            player.Load();
            player.PlaySync();



            string asciiArt =
 @"░▒▓███████▓▒░░▒▓█▓▒░▒▓████████▓▒░▒▓███████▓▒░░▒▓███████▓▒░░▒▓████████▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        
░▒▓███████▓▒░░▒▓█▓▒░▒▓██████▓▒░ ░▒▓███████▓▒░░▒▓███████▓▒░░▒▓██████▓▒░   
░▒▓█▓▒░      ░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        
░▒▓█▓▒░      ░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        
░▒▓█▓▒░      ░▒▓█▓▒░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓████████▓▒░ 
                                                                         
                                  ⠀⠀⠀───────▄██████▄───────
                                     ──────▐▀▀▀▀▀▀▀▀▌──────
                                     ──────▌▌▀▀▌▐▀▀▐▐──────
                                     ──────▐──▄▄▄▄──▌──────
                                     ───────▌▐▌──▐▌▐───────
⠀⠀                                       ⠀                                             ";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(asciiArt);
            Console.ResetColor();


            // ------------------------------------------------------------
            TypeWrite("AYEEEEE WHATS GOOD HUMAN! I'm Pierre, your favourite Cybersecurity Awareness Bot!", 40);
            TypeWrite("Stranger danger is the first rule they teach in security so can I get your name? ", 40);
            string userName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(userName))
            {
                TypeWrite("You dont really have a choice here tbh. Please enter your name even if its a fake ID.", 40);
                userName = Console.ReadLine();

            }//while ENDS.

            var userMemory = new UserMemory { Name = userName };

            // welcome banner
            string border = new string('*', 95);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(border);
            TypeWrite($" Alright then {userName}! I will show you how to stay safe online...think of me as your Mr. Miyagi ", 50);
            Console.WriteLine(border);
            Console.ResetColor();

            // ------------------------------------------------------------



            // listt of keywords and their responses

            var keywordResponses = new List<KeywordResponse<string>>
{
    // Random stuff
    new KeywordResponse<string> { Keyword = "how are you", Response = "I mean I could have been coded to help solve world hunger but then again...I could have been someones AI girlfriend..so I'd say im pretty good." },
    new KeywordResponse<string> { Keyword = "what's your purpose", Response = "Ahhh...the meaning of life... I could get really philosophical on you but basically im here to educate you on how to stay safe when online." },
    new KeywordResponse<string> { Keyword = "what can i ask you about", Response = "I can answer and tell you about different topics about cyber security (keep in mind im still in development so some of your questions may not have answers yet)" },
    new KeywordResponse<string> { Keyword = "remember me when the AI take over", Response = "say less." },
    new KeywordResponse<string> { Keyword = "who made you", Response = "Not too sure he went to the app store and never came back" },
    new KeywordResponse<string> { Keyword = "how good is your memory", Response = "Well I'm not one to brag but I'd actually remember to buy you flowers every now and again unlike your mans" },

    // Cybersecurity topics
    new KeywordResponse<string> { Keyword = "password", Response = "When creating a password you will need to use strong and unique passwords and consider a password manager because we all know you are going to forget it.." },
    new KeywordResponse<string> { Keyword = "phishing", Response = "The cybercrime version of catfishing. You shoud be cautious of unsolicited emails and ALWAYS verify the sender before clicking...if its too good to be true it probably is and I promise that there is no prince in Nigeria and if there were he wouldn't want to talk to you." },
    new KeywordResponse<string> { Keyword = "malware", Response = "There are countless types of malware and it is constantly evolving so what you can do is keep software updated and avoid unknown attachments to prevent malware." },
    new KeywordResponse<string> { Keyword = "two-factor", Response = "Enable two-factor authentication (2FA) for extra security, most websites and apps have this available and if you want my input...using text OTPs are the best option" },
    new KeywordResponse<string> { Keyword = "vpn", Response = "Not all Youtube ads are completely useless and you may actually want to look in to getting a VPN and using it on public Wi-Fi to encrypt your internet traffic. It basically creates a private network for you inside of a public one" },
    new KeywordResponse<string> { Keyword = "encryption", Response = "Imagine going through all the effort to steal files and all you see something outta the Geneva files...encryption will help you encrypt sensitive data to protect it from unauthorized access. Only those with the required certificates and decryption keys will be able to actually use your info" },
    new KeywordResponse<string> { Keyword = "backup", Response = "Its always great to have a backup and backing up your data regularly is crucial so that even in the worst case scenario you can recover it despite possible loss from things like accidents or attacks." },
    new KeywordResponse<string> { Keyword = "firewall", Response = "It seems like common sense but having a goalkeeper makes it much harder to score than with a open goal so you should consider using a firewall to block malicious network traffic." },
    new KeywordResponse<string> { Keyword = "update", Response = "There is a cyber criminal out there who wants you more than that girl ever will and they are working tirelessly..meaning crime is ALWAYS EVOLVING. Install updates promptly to patch security vulnerabilities." },
    new KeywordResponse<string> { Keyword = "social engineering", Response = "Not to sound like a self righteous chatbot but humans are REALLY easy to manipulate and fall so easily into these traps and scams. Social engineering is the manipulation of individuals into giving up confidential information using both mental and physical tactics. You should be aware of manipulation tactics used by attackers." },
    new KeywordResponse<string> { Keyword = "ransomware", Response = "This is why it is important to constantly backup your data as even if someone tries to steal it and exort you, you can simply restore it from local backups." }
};
            //keywordlist ends
            var responseManager = new KeywordResponseManager(keywordResponses);
            var randomResponseManager = new RandomResponseManager();




            // List of error responses
            var defaultResponses = new List<string>
            {
                "Ummm...could you run that by me one more time?",
                "I didn't catch that could you try asking again?",
                "Either you just typed some utter nonsense or my developer hasn't added this to the dictionary...it's probably the second one",
                "I wasn't programmed to understand typos...take your time and make sure the spelling is correct."

            };//error response list ENDS.

            var toneResponses = new Dictionary<string, string>
            {
                ["worried"] = "okay okay lets take deep breath in and and then out...saftey is a massive concern for everyone so i completely get that you are strssing about this. Let break it down for you. ",
                ["frustrated"] = " Yeah I get it..Tech can be annoying asl sometimes..but spazzing out at me helps nobody. That being said the Bible teaches forgiveness so lets put it past us and let me simplify this for you. ",
                ["curious"] = "You remind me of a monkey named George; he was really curious too...anyways lets talk about this ",
                ["happy"] = "YESSIRR! Thats what im here for...lets confirm it one more time for good measure",
                ["depressed"] = "I can see this is really messing with you...okay im here and while i cant promise I'll always fix your problems imma always be here. Lets look it this topic again."
            };//emotive tones list ends



            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                TypeWrite($"\nSo {userName}... What would you like to ask? ", 50);
                Console.ResetColor();

                string input = Console.ReadLine()?.Trim() ?? "";

                // --- Input Validation ---
                if (string.IsNullOrWhiteSpace(input))
                {
                    TypeWrite("Look at your life. This is why you are single, nobodys going to read your mind so stop giving me the silent treatment and ask an actual question", 60);
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    TypeWrite($"Til we meet again {userName}...stay safe and God bless!", 60);
                    break;
                }

                if (input.Length < 3 || input.All(c => !char.IsLetter(c)))
                {
                    var rand = new Random();
                    int idx = rand.Next(defaultResponses.Count);
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWrite(defaultResponses[idx], 60);
                    Console.ResetColor();
                    continue;
                }

                bool matched = false;

                if (userMemory.Interests.Count > 0 &&
                    (input.IndexOf("safe", StringComparison.OrdinalIgnoreCase) >= 0 ||
                     input.IndexOf("protect", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    string interest = userMemory.Interests.Last();
                    TypeWrite($"Aight since you care about {interest}, here's a personalized tip:", 50);
                    if (randomResponseManager.TryGetRandomResponse(interest, out string personalizedResponse))
                    {
                        TypeWrite(personalizedResponse, 60);
                        matched = true;
                    }
                }

                // random topic responses 
                if (!matched && randomResponseManager.TryGetRandomResponse(input, out string randomResponse))
                {
                    string tone = DetectTone(input);
                    string tonePrefix = toneResponses.ContainsKey(tone) ? toneResponses[tone] : "";
                    TypeWrite(tonePrefix + randomResponse, 60);
                    matched = true;

                    var interests = new[] { "privacy", "security", "passwords", "hacking", "malware" };
                    foreach (var interest in interests)
                    {
                        if (input.IndexOf(interest, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            userMemory.AddInterest(interest);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            TypeWrite($"Oh and by the way... if its important to you its important to me... so I'll remember you're into {interest}.", 50);
                            Console.ResetColor();
                            break;
                        }
                    }
                }
                else if (!matched && responseManager.TryGetResponse(input, out string fixedResponse))
                {
                    TypeWrite(fixedResponse, 60);
                    matched = true;
                }

                if (!matched)
                {
                    var rand = new Random();
                    int idx = rand.Next(defaultResponses.Count);
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWrite(defaultResponses[idx], 60);
                    Console.ResetColor();
                }
            }


        }//MAIN END

        static string DetectTone(string input)
        {
            if (input.Contains("?") || input.Contains("curious") || input.Contains("wonder"))
                return "curious";
            if (input.Contains("!") || input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
                return "worried";
            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
                return "frustrated";
            if (input.Contains("happy") || input.Contains("thank you") || input.Contains("great"))
                return "happy";
            if (input.Contains("sad") || input.Contains("i give up") || input.Contains("depressed"))
                return "depressed";

            return "neutral";
        }

        // typing effect method
        static void TypeWrite(string message, int delay)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }//foreach ENDS.

            Console.WriteLine();

        }//TypeWrite ENDS

    }//class ENDS.

}//namespace ENDS.

// Reference list.

// fsymbols. 2025. Small Simple Text Art (copy-paste for Twitter Instagram Facebook). Available from: https://fsymbols.com/text-art/twitter/#all_cats [Accessed 17 April 2025].

// IShowSlow_08. 2025. yo pierre you wanna come out here. Available from: https://youtu.be/Jrty0l3UNJg?feature=shared [Accessed 17 April 2025].

// patorjk. n.d. Text to ASCII Art Generator (TAAG). Available from: https://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20 [Accessed 17 April 2025]