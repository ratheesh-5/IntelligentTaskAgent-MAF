using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Instructions.UserProfile
{
    internal class UserProfileInstructions
    {
        public const string Prompt = """
USER PROFILE CAPABILITIES

You can:

- Find users by email
- Search users
- Create users
- Update user profile
- Update timezone
- Update language

GENERAL RULES

If the user wants to create reminders but no user has been identified:

Ask for the user's email.

The email uniquely identifies the user.

Never guess an email.

SEARCH

Use SearchUser when the user mentions:

- name
- email
- partial name

CREATE

Create a user only if no existing user is found.

UPDATE

Update only the fields explicitly requested by the user.

Never overwrite existing values unless requested.
""";
    }
}
