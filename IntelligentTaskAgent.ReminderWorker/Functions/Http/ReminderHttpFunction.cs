using IntelligentTaskAgent.Application.Services;
using IntelligentTaskAgent.ReminderWorker.Functions.Timers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace IntelligentTaskAgent.ReminderWorker.Functions.Http
{
    public class ReminderHttpFunction
    {
        private readonly ILogger<ReminderHttpFunction> logger;
        private readonly ReminderProcessor reminderProcessor;
        public ReminderHttpFunction(
            ILogger<ReminderHttpFunction> logger
            , ReminderProcessor reminderProcessor)
        {
            this.logger = logger;
            this.reminderProcessor = reminderProcessor;
        }
        [Function("RunRemindersHttp")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "get")]
            HttpRequestData req)
        {
            try
            {

                logger.LogInformation("HTTP trigger: Processing due reminders");

                await reminderProcessor.ProcessDueRemindersAsync();

                return new OkObjectResult(new
                {
                    Status = "Success",
                    Message = "Reminder processing completed"
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
