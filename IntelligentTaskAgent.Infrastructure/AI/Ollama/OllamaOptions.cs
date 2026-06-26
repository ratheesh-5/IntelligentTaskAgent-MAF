using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Infrastructure.AI.Ollama
{
    public class OllamaOptions
    {
        public const string SectionName = "LLMModel:Ollama";
        public string BaseUrl { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }
}
