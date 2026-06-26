using IntelligentTaskAgent.Core.Context;
using IntelligentTaskAgent.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Agents.UserProfile
{
    public interface IUserProfileAgent
    {
        Task<AgentResponse> HandleAsync(
        AgentContext context,
        ConversationState state);
    }
}
