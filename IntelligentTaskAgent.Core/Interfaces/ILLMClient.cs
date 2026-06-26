using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Core.Interfaces
{
    public interface ILLMClient
    {
        Task<string> CompleteAsync(string prompt);
        string ModelName { get; }
    }
}
