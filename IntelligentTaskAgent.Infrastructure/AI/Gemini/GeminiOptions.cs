using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Infrastructure.AI.Gemini
{
    public class GeminiOptions
    {
        public const string SectionName = "LLMModel:Gemini";
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }
}
