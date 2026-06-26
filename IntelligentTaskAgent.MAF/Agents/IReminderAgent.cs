using Microsoft.Agents.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Models.Responses;

namespace IntelligentTaskAgent.MAF.Agents
{
    public interface IReminderAgent
    {
        Task<ConversationResponse> ChatAsync(
            ConversationRequest request,
            CancellationToken cancellationToken = default);
    }
}
