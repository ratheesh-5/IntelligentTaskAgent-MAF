using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.AI.Prompts
{
    public interface IUserProfilePromptProvider
    {
        Task<string> BuildIserProfileExtractionPromptAsync(
    string userInput);
    }
}
