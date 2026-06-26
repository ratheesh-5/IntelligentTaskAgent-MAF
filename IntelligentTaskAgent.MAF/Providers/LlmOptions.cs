using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Providers
{
    public class LlmOptions
    {
        public const string SectionName = "LLM";

        public string Provider { get; set; } = "Gemini";

        public string BaseUrl { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;
    }
}
