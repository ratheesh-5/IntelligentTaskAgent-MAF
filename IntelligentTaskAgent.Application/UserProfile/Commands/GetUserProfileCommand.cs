    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.UserProfile.Commands;

public sealed class GetUserProfileCommand
{
    public GetUserProfileCommand(
        string email)
    {
        Email = email;
    }

    public string Email { get; }
}
