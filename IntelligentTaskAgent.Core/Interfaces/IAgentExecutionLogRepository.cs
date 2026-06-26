using IntelligentTaskAgent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface IAgentExecutionLogRepository
    {
        Task AddAsync(AgentExecutionLogEntity log);

        Task UpdateTaskIdByCorrelationAsync(
            Guid correlationId,
            Guid taskId);
    }
}
