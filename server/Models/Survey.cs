namespace server.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public int ContactId { get; set; }
        public Contact? Contact { get; set; }

        public List<Answer> Answers { get; set; } = new();
    }
}
