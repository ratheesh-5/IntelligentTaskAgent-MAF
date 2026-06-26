using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Plugins
{
    public class NotificationPlugin
    {
        public string ToolName => "NotificationPlugin";

        public Task<object?> ExecuteAsync(object? input)
        {
            // TODO: send notification through configured channels
            return Task.FromResult<object?>(null);
        }
    }
}
