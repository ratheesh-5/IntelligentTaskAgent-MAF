using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Responses
{
    public sealed class SearchReminderResult
    {
        public bool Success { get; set; }

        public List<ReminderSummary> Reminders { get; set; } = [];

        public string Message { get; set; } = string.Empty;
    }
}
