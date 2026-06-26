using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Notifications.Infrastructure.Email
{
    public class EmailOptions
    {
        public const string SectionName = "Channel:Email";
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
    }
}
