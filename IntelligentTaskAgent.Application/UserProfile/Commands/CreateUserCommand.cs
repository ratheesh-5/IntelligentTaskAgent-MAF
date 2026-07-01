using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Application.UserProfile.Commands;

public sealed class CreateUserCommand
{
    public CreateUserCommand(
        string? name,
        string email)
    {
        Name = name;
        Email = email;
    }

    public string? Name { get; }

    public string Email { get; }
}
