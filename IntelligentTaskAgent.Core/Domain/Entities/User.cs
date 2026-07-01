namespace IntelligentTaskAgent.Core.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? TimeZone { get; set; }

        public string? Language { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
