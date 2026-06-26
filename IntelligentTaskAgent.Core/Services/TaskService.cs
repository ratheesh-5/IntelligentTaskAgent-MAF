using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
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
    }
}
