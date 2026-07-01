using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.RepositoryModels
{
    public sealed class ReminderProjection
    {
        public Guid TaskId { get; set; }

        public Guid ReminderId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? ReminderAt { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Channel { get; set; }
    }
}
