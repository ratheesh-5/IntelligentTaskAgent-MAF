namespace IntelligentTaskAgent.Core.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
