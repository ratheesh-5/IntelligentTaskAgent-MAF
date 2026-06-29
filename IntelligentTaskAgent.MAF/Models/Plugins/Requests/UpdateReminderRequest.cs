using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Plugins.Requests
{
    public sealed class UpdateReminderRequest
    {
        /// <summary>
        /// Task Id to update.
        /// </summary>
        public Guid TaskId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReminderAt { get; set; }

        public string? Channel { get; set; }
    }
}
