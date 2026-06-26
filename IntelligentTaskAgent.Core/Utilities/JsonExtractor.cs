using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelligentTaskAgent.Core.Utilities
{
    public class JsonExtractor
    {
        public static string ExtractJson(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                throw new InvalidOperationException("Empty LLM response");

            // Remove markdown code fences ```json ```
            raw = raw.Replace("```json", "")
                     .Replace("```", "")
                     .Trim();

            // Extract first JSON object
            var match = Regex.Match(raw, @"\{[\s\S]*\}");

            if (!match.Success)
                throw new InvalidOperationException("No valid JSON found in LLM output");

            return match.Value;
        }
    }
}
