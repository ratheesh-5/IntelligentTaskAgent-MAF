using IntelligentTaskAgent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Services
{
    public interface ITaskService
    {
        Task AddAsync(TaskEntity taskEntity);
        Task<TaskEntity?> SearchByTaskIdAsync(Guid taskId);
        Task<List<TaskEntity>> SearchByTaskTitleAsync(string title);
        Task UpdateAsync(TaskEntity taskEntity);
        Task DeleteAsync(Guid taskId);
        Task<List<TaskEntity>> SearchAsync(string keyword);
    }
}
