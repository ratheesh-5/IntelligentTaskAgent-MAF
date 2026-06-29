using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Plugins.Requests
{
    public sealed class CreateReminderRequest
    {
        [Description("Short title of the reminder.")]
        public string Title { get; set; } = string.Empty;

        [Description("Reminder description.")]
        public string Description { get; set; } = string.Empty;

        [Description("Reminder date and time in UTC.")]
        public DateTime? ReminderAt { get; set; }

        [Description("Notification channel such as Email or Telegram.")]
        public string? Channel { get; set; }

        /// <summary>
        /// Original user message received by the AI.
        /// Used for audit/debugging.
        /// </summary>

        [Description("Original user message received by the AI")]
        public string RawUserInput { get; set; } = string.Empty;
    }
}
