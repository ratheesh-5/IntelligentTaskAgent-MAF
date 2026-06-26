using IntelligentTaskAgent.Core.Agents;
using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Services
{
    public class TaskOrchestrationService
    {
        private readonly TaskAgent taskAgent;
        private readonly ITaskRepository taskRepository;
        private readonly IReminderRepository reminderRepository;
        private readonly IAgentExecutionLogRepository agentExecutionLogRepository;

        private static readonly Guid TestUserId =
                    Guid.Parse("169018FB-C4E4-4896-983D-B6C6EBEFED03");

        public TaskOrchestrationService(
            TaskAgent taskAgent
            , ITaskRepository taskRepository
            , IReminderRepository reminderRepository
            , IAgentExecutionLogRepository agentExecutionLogRepository)
        {
            this.taskAgent = taskAgent;
            this.taskRepository = taskRepository;
            this.reminderRepository = reminderRepository;
            this.agentExecutionLogRepository = agentExecutionLogRepository;
        }

        public async Task HandleAsync(string userInput)
        {
            var (intent, correlationId) = await taskAgent.AnalyzeAsync(userInput);

            var task = new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = intent.TaskTitle,
                Description = intent.Description,
                DueDate = intent.ReminderAt,
                Status = "Pending",
                RawUserInput = userInput,
            };

            await taskRepository.AddAsync(task);

            // 🔁 UPDATE existing agent logs with TaskId
            await agentExecutionLogRepository.UpdateTaskIdByCorrelationAsync(
                correlationId,
                task.TaskId);

            if (intent.ReminderAt.HasValue && intent.ReminderAt.Value > DateTime.UtcNow)
            {
                var reminder = new ReminderEntity
                {
                    ReminderId = Guid.NewGuid(),
                    TaskId = task.TaskId,
                    NotifyAt = intent.ReminderAt.Value,
                    Channel = intent.Channel, // or dynamically based on preference
                    IsSent = false,
                    // TODO : added for now in future done by code
                    UserId = TestUserId,
                    // TODO message can de prepared different place
                    Message = intent.Description
                };

                await reminderRepository.AddAsync(reminder);
            }
        }
        public async Task HandleAsync(string rawUserInput, TaskIntentResult intent)
        {
            var task = new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = intent.TaskTitle,
                Description = intent.Description,
                DueDate = intent.ReminderAt,
                Status = "Pending",
                RawUserInput = rawUserInput,
            };

            await taskRepository.AddAsync(task);

            if (intent.ReminderAt.HasValue && intent.ReminderAt.Value > DateTime.UtcNow)
            {
                var reminder = new ReminderEntity
                {
                    ReminderId = Guid.NewGuid(),
                    TaskId = task.TaskId,
                    NotifyAt = intent.ReminderAt.Value,
                    Channel = intent.Channel, // or dynamically based on preference
                    IsSent = false,
                    // TODO : added for now in future done by code
                    UserId = TestUserId,
                    // TODO message can de prepared different place
                    Message = intent.Description
                };

                await reminderRepository.AddAsync(reminder);
            }
        }
    }
}
