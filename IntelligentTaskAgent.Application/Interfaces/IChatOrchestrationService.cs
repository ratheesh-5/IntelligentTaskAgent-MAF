using IntelligentTaskAgent.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Interfaces
{
    public interface IChatOrchestrationService
    {
        Task<string> HandleMessageAsync(
            AgentContext context,
            CancellationToken ct);
    }
}
