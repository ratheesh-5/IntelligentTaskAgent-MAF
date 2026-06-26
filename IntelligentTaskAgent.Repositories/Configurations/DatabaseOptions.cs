using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Repositories.Configurations
{
    public class DatabaseOptions
    {
        public const string SectionName = "Database";
        public string SQLConnectionString { get; set; } = string.Empty;
    }
}
