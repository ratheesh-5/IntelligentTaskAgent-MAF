using IntelligentTaskAgent.MAF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Memory
{
    public sealed class ConversationContextService
     : IConversationContextService
    {
        private readonly IConversationMemory conversationMemory;

        public ConversationContextService(
            IConversationMemory conversationMemory)
        {
            this.conversationMemory = conversationMemory;
        }

        public async Task<ConversationContext> GetOrCreateContextAsync(
            string? conversationId,
            CancellationToken cancellationToken = default)
        {
            conversationId = string.IsNullOrWhiteSpace(conversationId)
                ? Guid.NewGuid().ToString()
                : conversationId;

            var context =
                await conversationMemory.GetAsync(conversationId);

            if (context != null)
            {
                return context;
            }

            return new ConversationContext
            {
                ConversationId = conversationId,
                WorkflowState = AgentWorkflowState.Unknown
            };
        }

        public Task SaveContextAsync(
            ConversationContext context)
        {
            return conversationMemory.SaveAsync(context);
        }

        public Task RemoveContextAsync(
            string conversationId)
        {
            return conversationMemory.RemoveAsync(conversationId);
        }
    }
}
