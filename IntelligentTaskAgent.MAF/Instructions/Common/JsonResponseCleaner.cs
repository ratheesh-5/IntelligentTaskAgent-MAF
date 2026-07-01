namespace IntelligentTaskAgent.MAF.Common;

public static class JsonResponseCleaner
{
    public static string Clean(string response)
    {
        if (string.IsNullOrWhiteSpace(response))
        {
            throw new InvalidOperationException("Router returned an empty response.");
        }

        response = response.Trim();

        if (response.StartsWith("```"))
        {
            var lines = response
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            // Remove opening fence (``` or ```json)
            if (lines.Count > 0 && lines[0].StartsWith("```"))
            {
                lines.RemoveAt(0);
            }

            // Remove closing fence
            if (lines.Count > 0 && lines[^1].StartsWith("```"))
            {
                lines.RemoveAt(lines.Count - 1);
            }

            response = string.Join(Environment.NewLine, lines);
        }

        return response.Trim();
    }
}