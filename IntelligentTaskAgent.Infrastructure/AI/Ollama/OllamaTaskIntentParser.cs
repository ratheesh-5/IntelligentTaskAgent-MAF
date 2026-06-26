using IntelligentTaskAgent.Core.AI.Models;
using IntelligentTaskAgent.Core.AI.Parsers;
using IntelligentTaskAgent.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IntelligentTaskAgent.Infrastructure.AI.Ollama
{
    public class OllamaTaskIntentParser : ILLMResponseParser<TaskIntentResult>
    {
        public TaskIntentResult Parse(string rawResponse)
        {
            var wrapper = JsonSerializer.Deserialize<OllamaResponseWrapper>(
                rawResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (wrapper == null || string.IsNullOrWhiteSpace(wrapper.Response))
                return Fallback(rawResponse);

            // ✅ CLEAN JSON BEFORE DESERIALIZATION
            var cleanJson = JsonExtractor.ExtractJson(wrapper.Response);

            return JsonSerializer.Deserialize<TaskIntentResult>(
                       cleanJson,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? Fallback(wrapper.Response);
        }
        private static TaskIntentResult Fallback(string input) =>
            new()
            {
                Intent = "Unknown",
                Description = input
            };
    }
}
