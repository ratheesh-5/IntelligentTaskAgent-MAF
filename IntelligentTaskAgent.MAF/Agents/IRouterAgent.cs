using IntelligentTaskAgent.MAF.Models.Requests;
using IntelligentTaskAgent.MAF.Routing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Agents
{
    public interface IRouterAgent
    {
        Task<AgentRouteResult> RouteAsync(
        ConversationRequest request,
        CancellationToken cancellationToken = default);
    }
}
