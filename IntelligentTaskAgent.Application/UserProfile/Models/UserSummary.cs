using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.UserProfile.Models;

public sealed class UserSummary
{
    public Guid UserId { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = string.Empty;
}
