using System;

namespace PierreCyberSecurityBotPROG
{
    public class ActivityLogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
