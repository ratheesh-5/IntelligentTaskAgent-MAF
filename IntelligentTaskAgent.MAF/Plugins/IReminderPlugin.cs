using IntelligentTaskAgent.MAF.Models.Plugins.Requests;
using IntelligentTaskAgent.MAF.Models.Plugins.Responses;
using IntelligentTaskAgent.MAF.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Plugins
{
    public interface IReminderPlugin
    {
        Task<ReminderResult> CreateReminderAsync(
        CreateReminderRequest request);

        Task<ReminderResult> UpdateReminderAsync(
        UpdateReminderRequest request);

        Task<bool> DeleteReminderAsync(
            Guid taskId);

        Task<SearchReminderResult> SearchReminderAsync(
    SearchReminderRequest request);
    }
}
