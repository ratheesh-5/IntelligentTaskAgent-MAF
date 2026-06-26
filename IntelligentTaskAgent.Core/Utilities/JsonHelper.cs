using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IntelligentTaskAgent.Core.Utilities
{
    public static class JsonHelper
    {
        public static bool IsValidJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                JsonDocument.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
