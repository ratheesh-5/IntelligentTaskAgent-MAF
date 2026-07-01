using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Memory
{
    public interface IConversationContextService
    {
        Task<ConversationContext> GetOrCreateContextAsync(
            string? conversationId,
            CancellationToken cancellationToken = default);

        Task SaveContextAsync(
            ConversationContext context);

        Task RemoveContextAsync(
            string conversationId);
    }
}
