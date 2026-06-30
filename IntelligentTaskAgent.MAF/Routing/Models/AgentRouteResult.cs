using IntelligentTaskAgent.MAF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Routing.Models
{
    public sealed class AgentRouteResult
    {
        public AgentType AgentType { get; set; }

        public string Intent { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public double Confidence { get; set; }
    }
}
