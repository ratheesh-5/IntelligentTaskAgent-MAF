using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentTaskAgent.MAF.Models.Requests
{
    public sealed class ConversationRequest
    {
        /// <summary>
        /// User's message.
        /// </summary>
        public required string Message { get; init; }

        /// <summary>
        /// Existing conversation ID.
        /// </summary>
        public string? ConversationId { get; init; }

        /// <summary>
        /// Optional user identifier.
        /// Will be used later for memory, user preferences,
        /// and profile retrieval.
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// Optional tenant identifier for multi-tenant support.
        /// </summary>
        public string? TenantId { get; init; }
    }
}
