using IntelligentTaskAgent.Core.AI.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Infrastructure.AI.Gemini
{
    public sealed class GeminiJsonResponseParser<T>
        : ILLMResponseParser<T>
    {
        public T Parse(string rawResponse)
        {
            using var doc = JsonDocument.Parse(rawResponse);

            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (string.IsNullOrWhiteSpace(text))
                throw new InvalidOperationException("Gemini response text is empty");

            var cleanJson = CleanJson(text);

            return JsonSerializer.Deserialize<T>(
                cleanJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    }
                })!;
        }

        private static string CleanJson(string text)
        {
            text = text.Trim();

            if (text.StartsWith("```", StringComparison.Ordinal))
            {
                text = text
                    .Replace("```json", "", StringComparison.OrdinalIgnoreCase)
                    .Replace("```", "")
                    .Trim();
            }

            return text;
        }
    }
}
