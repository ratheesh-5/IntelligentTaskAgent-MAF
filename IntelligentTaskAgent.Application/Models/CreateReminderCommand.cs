using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Models
{
    public sealed class CreateReminderCommand
    {
        /// <summary>
        /// User Id who owns the reminder
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Short reminder title
        /// Example: "Dinner"
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Reminder description
        /// Example: "Remind me to have dinner"
        /// </summary>
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// Reminder date/time in UTC
        /// </summary>
        public DateTime? ReminderAt { get; init; }

        /// <summary>
        /// Notification channel selected by AI/User
        /// Email / Telegram / WhatsApp / SMS / null
        /// </summary>
        public string? Channel { get; init; }

        /// <summary>
        /// Original user message before AI processing
        /// </summary>
        public string RawUserInput { get; init; } = string.Empty;
    }
}
