using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Services
{
    public class AgentLoggingService
    {
        private readonly IAgentExecutionLogRepository agentExecutionLogRepository;

        public AgentLoggingService(
            IAgentExecutionLogRepository agentExecutionLogRepository)
        {
            this.agentExecutionLogRepository = agentExecutionLogRepository;
        }

        public async Task LogAsync(
            Guid correlationId,
            string userInput,
            string agentAction,
            string modelUsed,
            string? output = null,
            bool success = true,
            string? errorMessage = null,
            Guid? taskId = null)
        {
            var log = new AgentExecutionLogEntity
            {
                CorrelationId = correlationId,
                TaskId = taskId,
                UserInput = userInput,
                AgentAction = agentAction,
                ModelUsed = modelUsed,
                Output = output ?? string.Empty,
                Success = success,
                ErrorMessage = errorMessage
            };

            await agentExecutionLogRepository.AddAsync(log);
        }

    }
}
