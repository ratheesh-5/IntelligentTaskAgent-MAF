using IntelligentTaskAgent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity taskEntity);
        Task<TaskEntity?> SearchByTaskIdAsync(Guid taskId);
        Task<List<TaskEntity>> SearchByTaskTitleAsync(string title);
        Task UpdateAsync(TaskEntity taskEntity);

        Task DeleteAsync(Guid taskId);

        Task<List<TaskEntity>> SearchAsync(string keyword);

    }
}
