using Microsoft.Agents.AI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Memory
{
    public sealed class InMemoryConversationStore
    : IConversationMemory
    {
        private readonly ConcurrentDictionary<
    string,
    ConversationContext> conversations = new();
        public Task<ConversationContext?> GetAsync(
    string conversationId)
        {
            conversations.TryGetValue(
                conversationId,
                out var context);

            return Task.FromResult(context);
        }

        public Task SaveAsync(
     ConversationContext context)
        {
            conversations[context.ConversationId] = context;

            return Task.CompletedTask;
        }

        public Task RemoveAsync(
    string conversationId)
        {
            conversations.TryRemove(
                conversationId,
                out _);

            return Task.CompletedTask;
        }
    }
}
