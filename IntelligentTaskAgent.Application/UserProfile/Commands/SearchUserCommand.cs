namespace IntelligentTaskAgent.Application.UserProfile.Commands;

public sealed class SearchUserCommand
{
    public SearchUserCommand(
        string? keyword,
        bool onlyActive = true,
        int? top = null)
    {
        Keyword = keyword;
        OnlyActive = onlyActive;
        Top = top;
    }

    public string? Keyword { get; }

    public bool OnlyActive { get; }

    public int? Top { get; }
}