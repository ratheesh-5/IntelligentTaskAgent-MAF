namespace IntelligentTaskAgent.Core.Context;

public class AgentContext
{
    public Guid UserId { get; set; }
    public long ChatId { get; set; }
    public string Message { get; set; } = string.Empty;

    // future: culture, timezone, ui-source (telegram/web)
}
