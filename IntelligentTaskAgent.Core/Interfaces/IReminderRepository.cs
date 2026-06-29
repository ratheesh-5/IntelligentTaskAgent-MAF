using IntelligentTaskAgent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface IReminderRepository
    {
        Task AddAsync(ReminderEntity reminder);

        Task UpdateAsync(ReminderEntity reminder);

        Task<IEnumerable<ReminderEntity>> GetPendingAsync(DateTime utcNow);

        Task DeleteReminderAsync(Guid taskId);

        Task<ReminderEntity?> SearchByTaskIdAsync(Guid taskId);

    }
}
