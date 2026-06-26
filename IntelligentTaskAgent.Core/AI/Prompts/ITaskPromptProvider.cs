using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.AI.Prompts
{
    public interface ITaskPromptProvider
    {
        Task<string> BuildTaskExtractionPromptAsync(
            string userInput,
            DateTime nowUtc);
    }
}
