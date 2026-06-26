using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Core.AI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using taskService = IntelligentTaskAgent.Core.Services.TaskOrchestrationService;
namespace IntelligentTaskAgent.Application.Services
{
    public class TaskOrchestrationService : ITaskOrchestrationService
    {
        private readonly taskService taskService;
        public TaskOrchestrationService(taskService taskService)
        {
            this.taskService = taskService;
        }
        public async Task HandleAsync(string rawUserInput, TaskIntentResult intent)
        {
            await this.taskService.HandleAsync(rawUserInput, intent);
        }
    }
}
