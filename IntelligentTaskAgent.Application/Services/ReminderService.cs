using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Application.Models;
using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Services
{
    public class ReminderService : IReminderService
    {
        private readonly ITaskRepository taskRepository;
        private readonly IReminderRepository reminderRepository;

        public ReminderService(ITaskRepository taskRepository,
            IReminderRepository reminderRepository)
        {
            this.taskRepository = taskRepository;
            this.reminderRepository = reminderRepository;
        }

        public async Task AddAsync(TaskEntity taskEntity)
        {
            await this.taskRepository.AddAsync(taskEntity);
        }

        public async Task DeleteAsync(Guid taskId)
        {
            await this.taskRepository.DeleteAsync(taskId);
        }

        public async Task<List<TaskEntity>> SearchAsync(string keyword)
        {
            return await this.taskRepository.SearchAsync(keyword);
        }

        public async Task<TaskEntity?> SearchByTaskIdAsync(Guid taskId)
        {
            return await this.taskRepository.SearchByTaskIdAsync(taskId);
        }

        public async Task<List<TaskEntity>> SearchByTaskTitleAsync(string title)
        {
            return await this.taskRepository.SearchByTaskTitleAsync(title);
        }

        public async Task UpdateAsync(TaskEntity taskEntity)
        {
            await this.taskRepository.UpdateAsync(taskEntity);
        }

        public async Task<Guid> CreateReminderAsync(CreateReminderCommand command)
        {
            var task = BuildTask(command);

            await this.taskRepository.AddAsync(task);

            var reminder = BuildReminder(command, task);
            reminder.UserId = new Guid("95EE7F81-B46B-444F-A8EF-1D1A30575F03");

            await this.reminderRepository.AddAsync(reminder);

            return task.TaskId;
        }
        private TaskEntity BuildTask(CreateReminderCommand command)
        {
            return new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = command.Title,
                Description = command.Description,
                DueDate = command.ReminderAt,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                RawUserInput = command.RawUserInput
            };
        }
        private ReminderEntity BuildReminder(
    CreateReminderCommand command,
    TaskEntity task)
        {
            return new ReminderEntity
            {
                ReminderId = Guid.NewGuid(),
                TaskId = task.TaskId,
                UserId = command.UserId,
                NotifyAt = command.ReminderAt ?? task.DueDate ?? DateTime.UtcNow,
                Channel = command.Channel,
                Message = task.Description,
                IsSent = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task UpdateReminderAsync(UpdateReminderCommand command)
        {
            var task = await this.taskRepository.SearchByTaskIdAsync(command.TaskId);

            if (task == null)
            {
                throw new InvalidOperationException("Task not found.");
            }

            if (!string.IsNullOrWhiteSpace(command.Title))
            {
                task.Title = command.Title.Trim();
            }

            if (!string.IsNullOrWhiteSpace(command.Description))
            {
                task.Description = command.Description.Trim();
            }

            if (command.ReminderAt.HasValue)
            {
                task.DueDate = command.ReminderAt.Value;
            }

            if (!string.IsNullOrWhiteSpace(command.RawUserInput))
            {
                task.RawUserInput = command.RawUserInput;
            }

            await this.taskRepository.UpdateAsync(task);

            var reminder = await this.reminderRepository.SearchByTaskIdAsync(command.TaskId);

            if (reminder != null)
            {
                if (command.ReminderAt.HasValue)
                {
                    reminder.NotifyAt = command.ReminderAt.Value;
                }

                if (!string.IsNullOrWhiteSpace(command.Channel))
                {
                    reminder.Channel = command.Channel;
                }

                reminder.Message = task.Description;

                await this.reminderRepository.UpdateAsync(reminder);
            }
        }

        public async Task DeleteReminderAsync(Guid taskId)
        {
            var task = await this.taskRepository.SearchByTaskIdAsync(taskId);

            if (task == null)
            {
                throw new InvalidOperationException("Task not found.");
            }

            await this.reminderRepository.DeleteReminderAsync(taskId);

            await this.taskRepository.DeleteAsync(taskId);
        }
    }
}
