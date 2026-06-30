using IntelligentTaskAgent.MAF.Agents;
using IntelligentTaskAgent.MAF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.AgentFactory
{
    public interface IAgentFactory
    {
        IAgent GetAgent(
            AgentType type);
    }
}
