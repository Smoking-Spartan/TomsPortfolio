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
        public DateTime? DeliveredAt { get; set; }
        public string? ErrorMessage { get; set; }

    }
    public enum MessageStatus
    {
        Pending,
        Sent,
        Delivered,
        Failed
    }

    public enum MessageType
    {
        Survey,
        Reminder,
        OptInConfirmation,
        OptOutConfirmation
    }
}
