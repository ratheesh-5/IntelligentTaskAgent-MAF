using IntelligentTaskAgent.Core.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.Interfaces
{
    public interface ITaskOrchestrationService
    {
        Task HandleAsync(string rawUserInput, TaskIntentResult intent);
    }
}
