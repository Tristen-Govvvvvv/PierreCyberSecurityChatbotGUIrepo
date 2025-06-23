using System;
using System.Collections.Generic;
using System.Linq;

namespace PierreCyberSecurityBotPROG
{
    public class UserMemory
    {
        public string Name { get; set; }
        public List<string> Interests { get; } = new List<string>();
        public List<string> LearnedTopics { get; } = new List<string>();

        public void AddInterest(string topic)
        {
            if (!Interests.Contains(topic))
                Interests.Add(topic);
        }

        public void AddLearnedTopic(string topic)
        {
            if (!LearnedTopics.Contains(topic))
                LearnedTopics.Add(topic);
        }

        public bool HasInterest(string topic)
        {
            return Interests.Any(i => i.Equals(topic, StringComparison.OrdinalIgnoreCase));
        }
    }
}