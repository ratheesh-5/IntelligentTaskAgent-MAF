using IntelligentTaskAgent.MAF.AgentFactory;
using IntelligentTaskAgent.MAF.Agents;
using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Models.Responses;
using IntelligentTaskAgent.MAF.Routing;
using Microsoft.Agents.AI;

namespace IntelligentTaskAgent.MAF.Runtime
{
    public sealed class AgentRuntime
    : IAgentRuntime
    {
        private readonly IRouterAgent routerAgent;

        private readonly IAgentFactory factory;

        public AgentRuntime(
            IRouterAgent routerAgent,
            IAgentFactory factory)
        {
            this.routerAgent = routerAgent;
            this.factory = factory;
        }

        public async Task<ConversationResponse> ChatAsync(
            ConversationRequest request,
            CancellationToken cancellationToken = default)
        {
            var route =
                await routerAgent.RouteAsync(
                    request,
                    cancellationToken);

            var agent =
                factory.GetAgent(route.AgentType);

            return await agent.ChatAsync(
                request,
                cancellationToken);
        }
    }
}
