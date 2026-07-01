using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.Core.RepositoryModels
{
    public sealed class UserSearchCriteria
    {
        public string? Keyword { get; set; }

        public bool OnlyActive { get; set; } = true;

        public int? Top { get; set; }
    }
}
