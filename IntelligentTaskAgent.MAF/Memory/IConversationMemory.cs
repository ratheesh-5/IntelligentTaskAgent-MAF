using Microsoft.Agents.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Memory
{
    public interface IConversationMemory
    {
        Task<AgentSession?> GetAsync(
            string conversationId);

        Task SaveAsync(
            string conversationId,
            AgentSession session);

        Task RemoveAsync(
            string conversationId);
    }
}
