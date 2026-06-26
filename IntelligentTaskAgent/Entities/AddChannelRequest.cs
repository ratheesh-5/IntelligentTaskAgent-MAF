namespace IntelligentTaskAgent.Entities
{
    public class AddChannelRequest
    {
        public string Channel { get; set; } = string.Empty;
        // Email | Telegram | SMS

        public string ChannelValue { get; set; } = string.Empty;
        // email / chatId / phone

        public bool IsPrimary { get; set; }
    }
}
