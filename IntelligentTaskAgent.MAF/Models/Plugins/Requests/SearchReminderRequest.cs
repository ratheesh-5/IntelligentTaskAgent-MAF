using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Plugins.Requests
{
    public sealed class SearchReminderRequest
    {
        /// <summary>
        /// Search keyword.
        /// Example:
        /// "Dinner"
        /// "Meeting"
        /// "Electricity"
        /// </summary>
        public string Query { get; set; } = string.Empty;
    }
}
