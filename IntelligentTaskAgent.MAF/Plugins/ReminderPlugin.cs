using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Plugins
{
    public class ReminderPlugin
    {
        public string ToolName => "ReminderPlugin";

        public Task<object?> ExecuteAsync(object? input)
        {
            // TODO: implement reminder scheduling
            return Task.FromResult<object?>(null);
        }
    }
}
