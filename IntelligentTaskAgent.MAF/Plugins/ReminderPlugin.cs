using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Application.Models;
using IntelligentTaskAgent.Application.Services;
using IntelligentTaskAgent.MAF.Models.Plugins.Requests;
using IntelligentTaskAgent.MAF.Models.Plugins.Responses;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;


namespace IntelligentTaskAgent.MAF.Plugins
{
    public sealed class ReminderPlugin : IReminderPlugin
    {
        private readonly IReminderService reminderService;

        public ReminderPlugin(IReminderService taskService)
        {
            this.reminderService = taskService;
        }

        public async Task<ReminderResult> CreateReminderAsync(
    CreateReminderRequest request)
        {
            var command = new CreateReminderCommand
            {
                // TODO:
                // Replace with authenticated user from JWT/Claims
                UserId = Guid.Empty,

                Title = request.Title,

                Description = request.Description,

                ReminderAt = request.ReminderAt,

                Channel = request.Channel,

                RawUserInput = string.IsNullOrWhiteSpace(request.RawUserInput)
                    ? request.Description
                    : request.RawUserInput
            };

            Guid taskId = await this.reminderService.CreateReminderAsync(command);

            return new ReminderResult
            {
                Success = true,
                TaskId = taskId,
                Message = $"Reminder '{request.Title}' created successfully."
            };
        }

        public async Task<ReminderResult> UpdateReminderAsync(
    UpdateReminderRequest request)
        {
            var command = new UpdateReminderCommand
            {
                TaskId = request.TaskId,
                Title = request.Title,
                Description = request.Description,
                ReminderAt = request.ReminderAt,
                Channel = request.Channel
            };

            await reminderService.UpdateReminderAsync(command);

            return new ReminderResult
            {
                Success = true,
                TaskId = request.TaskId,
                Message = "Reminder updated successfully."
            };
        }
        public async Task<bool> DeleteReminderAsync(
    Guid taskId)
        {
            await reminderService.DeleteReminderAsync(taskId);

            return true;
        }
    }
}
