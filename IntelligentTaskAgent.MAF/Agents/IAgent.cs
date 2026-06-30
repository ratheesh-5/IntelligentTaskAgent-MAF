using IntelligentTaskAgent.MAF.Enum;
using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Agents
{
    public interface IAgent
    {
        AgentType AgentType { get; }

        Task<ConversationResponse> ChatAsync(
            ConversationRequest request,
            CancellationToken cancellationToken = default);
    }
}
