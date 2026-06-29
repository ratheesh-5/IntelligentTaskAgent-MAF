using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Plugins.Responses
{
    public sealed class ReminderResult
    {
        public bool Success { get; set; }

        public Guid TaskId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
