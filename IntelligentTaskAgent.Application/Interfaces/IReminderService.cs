using IntelligentTaskAgent.Application.Models;
using IntelligentTaskAgent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Interfaces
{
    public interface IReminderService
    {
        Task AddAsync(TaskEntity taskEntity);
        Task<TaskEntity?> SearchByTaskIdAsync(Guid taskId);
        Task<List<TaskEntity>> SearchByTaskTitleAsync(string title);
        Task UpdateAsync(TaskEntity taskEntity);
        Task DeleteAsync(Guid taskId);
        Task<List<TaskEntity>> SearchAsync(string keyword);
        Task<Guid> CreateReminderAsync(CreateReminderCommand command);
        Task UpdateReminderAsync(UpdateReminderCommand command);

        Task DeleteReminderAsync(Guid taskId);

        Task<List<ReminderDto>> SearchReminderAsync(
    SearchReminderCommand command);
    }
}
