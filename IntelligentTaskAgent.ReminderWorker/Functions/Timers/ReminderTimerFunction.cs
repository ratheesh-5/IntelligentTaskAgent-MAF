using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelligentTaskAgent.Application.Services;

namespace IntelligentTaskAgent.ReminderWorker.Functions.Timers
{
    public class ReminderTimerFunction
    {
        private  readonly ILogger<ReminderTimerFunction> logger;
        private readonly ReminderProcessor reminderProcessor;
        public ReminderTimerFunction(
            ILogger<ReminderTimerFunction> logger
            , ReminderProcessor reminderProcessor)
        {
            this.logger = logger;
            this.reminderProcessor = reminderProcessor;
        }

        [Function("ReminderTimerFunction")]
        public async Task Run(
        [TimerTrigger("*/30 * * * * *")] TimerInfo timer)
        {
            try
            {
                // trigger every 30 seconds
                logger.LogInformation("Reminder timer triggered");

                await this.reminderProcessor.ProcessDueRemindersAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
            
        }

    }
}
