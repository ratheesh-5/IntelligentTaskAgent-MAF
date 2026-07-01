using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.UserProfile.Commands;

public sealed class UpdateUserProfileCommand
{
    public UpdateUserProfileCommand(
        Guid userId,
        string? name,
        string? timeZone,
        string? language)
    {
        UserId = userId;
        Name = name;
        TimeZone = timeZone;
        Language = language;
    }

    public Guid UserId { get; }

    public string? Name { get; }

    public string? TimeZone { get; }

    public string? Language { get; }
}