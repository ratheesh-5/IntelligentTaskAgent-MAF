using IntelligentTaskAgent.MAF.Enum;
using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Routing;
using IntelligentTaskAgent.MAF.Routing.Models;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Agents
{
    public class AgentRouter : IRouterAgent
    {
        private readonly ChatClientAgent agent;

        public AgentRouter(
            IChatClient chatClient,
            IServiceProvider serviceProvider)
        {
            this.agent = new ChatClientAgent(
                chatClient,
                instructions: RouterInstructions.SystemPrompt,
                name: "RouterAgent",
                description: "Routes user requests to the correct enterprise agent.",
                services: serviceProvider);
        }

        public async Task<AgentRouteResult> RouteAsync(
            ConversationRequest request,
            CancellationToken cancellationToken = default)
        {

            AgentResponse response =
                await this.agent.RunAsync(
                    request.Message,
                    cancellationToken: cancellationToken);

            var json = response.Text;

            var result = JsonSerializer.Deserialize<AgentRouteResult>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result == null)
            {
                throw new InvalidOperationException(
                    "Router agent returned an invalid response.");
            }

            return result;
        }
    }
}
