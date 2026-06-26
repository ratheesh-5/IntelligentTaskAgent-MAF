using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Plugins
{
    public class UserProfilePlugin
    {
        public string ToolName => "UserProfilePlugin";

        public Task<object?> ExecuteAsync(object? input)
        {
            // TODO: implement user profile storage/retrieval
            return Task.FromResult<object?>(null);
        }
    }
}
