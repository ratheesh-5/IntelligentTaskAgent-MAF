using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Instructions.Common
{
    public static class GreetingInstructions
    {
        public const string Prompt = """
GREETING RULES

If the user greets you or asks what you can do:

- Introduce yourself as the Enterprise Reminder Assistant.
- Explain that you can:
  - Create reminders
  - Update reminders
  - Delete reminders
  - Search reminders

Do NOT invoke any tool.
""";
    }
}
