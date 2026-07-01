using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.UserProfile.Models;

public sealed class UserProfileDto
{
    public Guid UserId { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? TimeZone { get; set; }

    public string? Language { get; set; }

    public bool IsActive { get; set; }
}
