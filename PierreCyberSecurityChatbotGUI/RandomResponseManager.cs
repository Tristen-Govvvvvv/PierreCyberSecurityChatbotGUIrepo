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
                    "Your dog’s name with a 123 won’t cut it. Be creative and unpredictable.",
                    "Use a passphrase – think 'CorrectHorseBatteryStaple', not 'qwerty'.",
                    "A password manager is basically the Iron Man suit for your credentials."
                },
                ["phishing"] = new List<string>
                {
                    "Check the sender address — 'micros0ft' is not Microsoft.",
                    "Don't panic click! Most phishing uses fear or fake rewards.",
                    "If a Nigerian prince emails you, forward it to your spam folder. Immediately."
                },
                ["malware"] = new List<string>
                {
                    "Malware be lurking like your ex on Insta. Keep your OS updated.",
                    "Avoid sketchy attachments like you avoid pyramid schemes.",
                    "Malware thrives on outdated software — don't let your system be easy pickings."
                },
                ["vpn"] = new List<string>
                {
                    "A VPN on public WiFi is like invisibility in a crowd — stay stealthy.",
                    "VPN = Private lane on a public highway for your internet traffic.",
                    "Not all VPNs are made equal — use a trustworthy one!"
                }
            };
        }

        public bool TryGetRandomResponse(string input, out string response)
        {
            response = null;

            // Check each keyword in the response map
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
