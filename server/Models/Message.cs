namespace server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string Content { get; set; } = "";
        public string? Url { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
