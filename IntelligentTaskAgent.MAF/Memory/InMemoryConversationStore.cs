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
        private readonly ConcurrentDictionary<string, AgentSession> sessions
            = new();

        public Task<AgentSession?> GetAsync(
            string conversationId)
        {
            sessions.TryGetValue(
                conversationId,
                out var session);

            return Task.FromResult(session);
        }

        public Task SaveAsync(
            string conversationId,
            AgentSession session)
        {
            sessions.AddOrUpdate(
                conversationId,
                session,
                (_, _) => session);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(
            string conversationId)
        {
            sessions.TryRemove(
                conversationId,
                out _);

            return Task.CompletedTask;
        }
    }
}
