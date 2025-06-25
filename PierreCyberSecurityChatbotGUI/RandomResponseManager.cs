using System;
using System.Collections.Generic;
using System.Linq;

namespace PierreCyberSecurityBotPROG
{
    public class RandomResponseManager
    {
        private readonly Dictionary<string, List<string>> responseMap;
        private readonly Random random;

        public RandomResponseManager()
        {
            random = new Random();

            responseMap = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new List<string>
                {
                    "When creating a password you will need to use strong and unique passwords and consider a password manager because we all know you are going to forget it..",
                    "Well for starters using 'password' as a password is not 1000IQ. I promise if you do your entires search history will be leaked. Use a passphrase like 'immaHalfManHalfHorse' instead or at least something of substance!",
                    "Password managers...USE THEM! They are like a bouncer for your accounts, except they never take bribes and help you remember your bajillion different passwords"
                },

                ["phishing"] = new List<string>
                {
                    "The cybercrime version of catfishing. You should be cautious of unsolicited emails and ALWAYS verify the sender before clicking...if its too good to be true it probably is and I promise that there is no prince in Nigeria and if there were he wouldn't want to talk to you.",
                    "Phishing emails are like fake lottery tickets - they promise millions but deliver malware.",
                    "Fearmongering is the reason for the majority of phishing attacks. Take a second and dont be so reactionary the chances are..the warning for your car insurance isnt legit..you dont have a car."
                },

                ["malware"] = new List<string>
                {
                    "There are countless types of malware and it is constantly evolving so what you can do is keep software updated and avoid unknown attachments to prevent malware.",
                    "Malware is like a digital STD - protection (antivirus) isn't optional anymore.",
                    "Fun fact: Some malware can turn your webcam on. Tape over it so we dont have to see some stuff only Diddy would want!"
                },

                ["two-factor"] = new List<string>
                {
                    "Enable two-factor authentication (2FA) for extra security, most websites and apps have this available and if you want my input...using text OTPs are the best option.",
                    "2FA is amazing as it strengthens the security provided by a password by a whole other layer of verification",
                    "Why should we use 2FA? Because NOT DOING SO is like leaving your car running with the doors unlocked in Compton."
                },

                ["vpn"] = new List<string>
                {
                    "Not all Youtube ads are completely useless - a VPN on public Wi-Fi encrypts your traffic like a digital invisibility cloak.",
                    "Using public WiFi without a VPN is like shouting your credit card number in a coffee shop. VPNs hide your IP address which protects your location and actvity from possible trackers",
                    "VPNs: Making your internet traffic look boring since 1996 (i know what you are)."
                },

                ["encryption"] = new List<string>
                {
                    "Imagine thieves breaking in to steal your files and finding something outta the Geneva Convention...that's encryption working its magic.",
                    "Encryption turns your data into a puzzle only you have the solution to. The only person who has the key to decrypting your data is you so PLEASE DONT TELL OR GIVE ANYINE THIS INFO",
                    "'UxwlrqvGrjudpdq' understand that? Thought so..well thats how any unauthourized person will see your data thanks to encryption."
                },

                ["backup"] = new List<string>
                {
                    "Backing up data is like insurance - boring until you desperately need it.",
                    "There is thing in a cybersecurity textbook called the 3-2-1 rule: 3 copies, 2 different media, 1 offsite. Your data will thank you.",
                    "No backups? Hope you enjoy explaining to your boss why the company files are gone LMAOO."
                },

                ["firewall"] = new List<string>
                {
                    "It seems like common sense but having a goalkeeper makes it much harder to score than with a open goal so you should consider using a firewall to block malicious network traffic.",
                    "No firewall? That's like leaving your front door wide open with a 'Hack Me' sign.",
                    "Even the best firewall can't stop you from clicking stupid links. Stay vigilant."
                },

                ["update"] = new List<string>
                {
                    "There is a cyber criminal out there who wants you more than that girl ever will and they are working tirelessly meaning crime is ALWAYS EVOLVING. Install updates promptly to patch security vulnerabilities.",
                    "Software updates are like vitamins for your computer - skip them and things get weak and outdated",
                    "We know you love procrastinating but save that for when you have to wash dishes...or do taxes. That update later button is basically a hack me button in disguise."
                },

                ["social engineering"] = new List<string>
                {
                    "Not to sound like a self righteous chatbot but humans are REALLY easy to manipulate and fall so easily into these traps and scams. Social engineering is the manipulation of individuals into giving up confidential information using both mental and physical tactics. You should be aware of manipulation tactics used by attackers",
                    "No amount of tech can stop someone from giving their password to a 'nice IT guy' on the phone.",
                    "If that guy/girl was able to manipulate you that easily then how much easier do you think it would be for a expert hacker"
                },

                ["ransomware"] = new List<string>
                {
                    "his is why it is important to constantly backup your data as even if someone tries to steal it and exort you, you can simply restore it from local backups.",
                    "The only good response to ransomware is restoring from backups. Everything else feeds the beast.",
                    "Pay the ransom and you're funding terrorism. Just restore from backups instead."
                },

                ["privacy"] = new List<string>
                {
                    "Lets be so real for a second...privacy is dead, but you can still make the corpse dance to your tune with good security.",
                    "I know I know...you think she will DM you if she sees that story but at least THINK before you post on social media..there could be valuable informtion that you are giving away in the backround.",
                    "Thanks to TikTok our attention spans are more cooked than ever but we should really lock in to read the privacy terms and permissions we give to things online."
                }
            };

        }

        public bool TryGetRandomResponse(string input, out string response)
        {
            response = null;

            
            foreach (var pair in responseMap)
            {
                if (input.IndexOf(pair.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var options = pair.Value;
                    if (options.Count > 0)
                    {
                        response = options[random.Next(options.Count)];
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
