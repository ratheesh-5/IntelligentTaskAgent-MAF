using IntelligentTaskAgent.Core.Agents.UserProfile;
using IntelligentTaskAgent.MAF.Agents;
using IntelligentTaskAgent.MAF.Enum;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUserProfileAgent = IntelligentTaskAgent.MAF.Agents.IUserProfileAgent;

namespace IntelligentTaskAgent.MAF.AgentFactory
{
    public sealed class AgentFactory
    : IAgentFactory
    {
        private readonly IServiceProvider services;

        public AgentFactory(
            IServiceProvider services)
        {
            this.services = services;
        }

        public IAgent GetAgent(AgentType type)
        {
            IAgent? agent = type switch
            {
                AgentType.Reminder =>
                    this.services.GetService<IReminderAgent>(),

                AgentType.UserProfile =>
                    this.services.GetService<IUserProfileAgent>(),

                AgentType.Notification =>
                    this.services.GetService<INotificationAgent>(),

                AgentType.Reporting =>
                    this.services.GetService<IReportingAgent>(),

                _ => null
            };

            return agent ?? this.services.GetRequiredService<IReminderAgent>();
        }
    }
}
