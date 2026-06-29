using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Models
{
    public sealed class UpdateReminderCommand
    {
        /// <summary>
        /// Existing Task Id
        /// </summary>
        public Guid TaskId { get; init; }

        /// <summary>
        /// User performing the update.
        /// Used for authorization/ownership validation.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Updated title.
        /// Null means do not change.
        /// </summary>
        public string? Title { get; init; }

        /// <summary>
        /// Updated description.
        /// Null means do not change.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Updated reminder time in UTC.
        /// Null means do not change.
        /// </summary>
        public DateTime? ReminderAt { get; init; }

        /// <summary>
        /// Updated notification channel.
        /// Null means do not change.
        /// </summary>
        public string? Channel { get; init; }

        /// <summary>
        /// Original user input.
        /// Useful for audit/history.
        /// </summary>
        public string? RawUserInput { get; init; }
    }
}
