using IntelligentTaskAgent.MAF.Agents;
using Microsoft.Agents.AI;

namespace IntelligentTaskAgent.MAF.Runtime
{
    public sealed class AgentRuntime : IAgentRuntime
    {
        public IReminderAgent ReminderAgent { get; }

        public AgentRuntime(IReminderAgent reminderAgent)
        {
            ReminderAgent = reminderAgent;
        }
    }
}
