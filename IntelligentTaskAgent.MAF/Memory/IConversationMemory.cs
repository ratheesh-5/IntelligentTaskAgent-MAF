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
        Task<ConversationContext?> GetAsync(
            string conversationId);

        Task SaveAsync(
            ConversationContext context);

        Task RemoveAsync(
            string conversationId);
    }
}
