using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Requests
{
    public sealed class SearchReminderRequest
    {
        public Guid? UserId { get; set; }

        public string? Keyword { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? Status { get; set; }

        public int? Top { get; set; }
    }
}
