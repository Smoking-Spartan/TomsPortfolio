using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class SurveyTemplate
    {
        public int Id { get; set; }
        
        [Column(TypeName = "timestamp with time zone")]
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }

        public int ContactId { get; set; }
        public Contact? Contact { get; set; }

        public List<Answer> Answers { get; set; } = new();
    }
}
